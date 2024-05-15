using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SuporteSolidario.ViewModel;
using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Application.Services;
using SuporteSolidarioBusiness.Application.UseCases;
using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Domain.Enums;

namespace SuporteSolidario.Controllers
{
    public class ColaboradorController : BaseController
    {
        private readonly ICryptoService _cryptoService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IColaboradorRepository _colaboradorRepository;
        private readonly ITokenService _tokenService;
        private readonly IGeoLocalizacaoService _geoLocalizacaoService;
        private readonly IAuthRepository _repo;

        public ColaboradorController(ICryptoService cryptoService, IColaboradorRepository colaboradorRepository, IUsuarioRepository usuarioRepository, ITokenService tokenService, IAuthRepository repo, IGeoLocalizacaoService geoLocalizacaoService)
        {
            _cryptoService = cryptoService;
            _usuarioRepository = usuarioRepository;
            _colaboradorRepository = colaboradorRepository;
            _tokenService = tokenService;
            _geoLocalizacaoService = geoLocalizacaoService;
            _repo = repo;
        }

        // GET: ColaboradorController
        public ActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync();
                return RedirectToAction("Login","Colaborador");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync();
                return RedirectToAction("Login","Colaborador");
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel vmLogin)
        {
            try
            {
                LoginDTO dto = new LoginDTO
                {
                    Login = vmLogin.Login,
                    Password1 = vmLogin.Password
                };

                EfetuarLoginUseCase useCase = new EfetuarLoginUseCase(_cryptoService, _tokenService, _repo, dto, TipoUsuario.Colaborador);
                List<Claim> lstClaims = useCase.Execute();

                var authProperties = new AuthenticationProperties
                {
                    RedirectUri = "/Colaborador/Index",
                    ExpiresUtc = vmLogin.RememberMe ? DateTime.UtcNow.AddYears(2) : DateTime.UtcNow.AddHours(1),
                    IsPersistent = true
                };

                var claimsIdentity = new ClaimsIdentity(lstClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Colaborador");
            }
            catch(Exception e)
            {
                ViewData["LoginInvalido"] = e.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult LoginInvalido()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                ViewData["FormAction"] = "Create";

                string valor = HttpContext.User.FindFirstValue("IdUsuario");
                if(string.IsNullOrEmpty(valor))
                {
                    return RedirectToAction("Index", "Colaborador");
                }

                long idUsuario = Int64.Parse(valor);

                ExisteColaboradorComIdUsuarioUseCase existeClienteComIdUsuario = new ExisteColaboradorComIdUsuarioUseCase(_colaboradorRepository, idUsuario);

                if(existeClienteComIdUsuario.Execute())
                {
                    return RedirectToAction("Index", "Colaborador");
                }

                ColaboradorViewModel viewModel = new ColaboradorViewModel();
                viewModel.IdUsuario = idUsuario;
    
                return View(viewModel);
            }
            catch (Exception e)
            {                
                ViewData["MensagemErro"] = e.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ColaboradorViewModel viewModel)
        {
            try
            {
                ColaboradorEntity input = ViewModelToEntity.MapColaborador(viewModel);

                AdicionarColaboradorUseCase useCase = new AdicionarColaboradorUseCase(
                    _geoLocalizacaoService,
                    _tokenService,
                    _colaboradorRepository,
                    _usuarioRepository,
                    input);

                useCase.Execute();

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View(viewModel);
            }
        }


        [HttpGet]
        public ActionResult Edit()
        {
            try
            {
                ViewData["FormAction"] = "Edit";

                string valor = HttpContext.User.FindFirstValue("IdUsuario");
                if(string.IsNullOrEmpty(valor))
                {
                    return RedirectToAction("Index", "Colaborador");
                }

                long id = Int64.Parse(valor);
    
                BuscarColaboradorUseCase useCase = new BuscarColaboradorUseCase(_colaboradorRepository, id);
    
                ColaboradorEntity entity = useCase.Execute();
    
                ColaboradorViewModel viewModel = EntityToViewModel.MapColaborador(entity);
    
                return View(viewModel);
            }
            catch (Exception e)
            {                
                ViewData["MensagemErro"] = e.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ColaboradorViewModel viewModel)
        {
            try
            {
                ColaboradorEntity input = ViewModelToEntity.MapColaborador(viewModel);
    
                AtualizarColaboradorUseCase useCase = new AtualizarColaboradorUseCase(_colaboradorRepository, input);
                useCase.Execute();
    
                return RedirectToAction("Index", "Colaborador");
            }
            catch (Exception e)
            {
                ViewData["MensagemErro"] = e.Message;
                return View();
            }            
        }
    }
}

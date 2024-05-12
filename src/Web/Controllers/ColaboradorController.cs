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

namespace SuporteSolidario.Controllers
{
    public class ColaboradorController : Controller
    {
        private readonly ICryptoService _cryptoService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IColaboradorRepository _colaboradorRepository;
        private readonly ITokenService _tokenService;
        private readonly IAuthRepository _repo;

        public ColaboradorController(ICryptoService cryptoService, IColaboradorRepository colaboradorRepository, IUsuarioRepository usuarioRepository, ITokenService tokenService, IAuthRepository repo)
        {
            _cryptoService = cryptoService;
            _usuarioRepository = usuarioRepository;
            _colaboradorRepository = colaboradorRepository;
            _tokenService = tokenService;
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

                EfetuarLoginUseCase useCase = new EfetuarLoginUseCase(_cryptoService, _tokenService, _repo, dto);
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
        public ActionResult LoginInvalido()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            try
            {
                ViewData["FormAction"] = "Edit";
    
                BuscarColaboradorUseCase useCase = new BuscarColaboradorUseCase(_colaboradorRepository, id);
    
                ColaboradorEntity colaboradorEntity = useCase.Execute();
    
                ColaboradorViewModel viewModel = EntityToViewModel.MapColaborador(colaboradorEntity);
    
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

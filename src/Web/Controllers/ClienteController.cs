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
    public class ClienteController : BaseController
    {
        private readonly ICryptoService _cryptoService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly ITokenService _tokenService;
        private readonly IGeoLocalizacaoService _geoLocalizacaoService;
        private readonly IAuthRepository _repo;

        public ClienteController(ICryptoService cryptoService, IUsuarioRepository usuarioRepository, IClienteRepository clienteRepository, ITokenService tokenService, IAuthRepository repo, IGeoLocalizacaoService geoLocalizacaoService)
        {
            _cryptoService = cryptoService;
            _usuarioRepository = usuarioRepository;
            _clienteRepository = clienteRepository;
            _tokenService = tokenService;
            _geoLocalizacaoService = geoLocalizacaoService;
            _repo = repo;
        }

        // GET: ClienteController
        public ActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync();
                return RedirectToAction("Login","Cliente");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync();
                return RedirectToAction("Login","Cliente");
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

                EfetuarLoginUseCase useCase = new EfetuarLoginUseCase(_cryptoService, _tokenService, _repo, dto, TipoUsuario.Cliente);
                List<Claim> lstClaims = useCase.Execute();

                var authProperties = new AuthenticationProperties
                {
                    RedirectUri = "/Cliente/Index",
                    ExpiresUtc = vmLogin.RememberMe ? DateTime.UtcNow.AddYears(2) : DateTime.UtcNow.AddHours(1),
                    IsPersistent = true
                };

                var claimsIdentity = new ClaimsIdentity(lstClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Cliente");
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
                    return RedirectToAction("Index", "Cliente");
                }

                long idUsuario = Int64.Parse(valor);

                ExisteClienteComIdUsuarioUseCase existeClienteComIdUsuario = new ExisteClienteComIdUsuarioUseCase(_clienteRepository, idUsuario);

                if(existeClienteComIdUsuario.Execute())
                {
                    return RedirectToAction("Index", "Cliente");
                }

                ClienteViewModel viewModel = new ClienteViewModel();
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
        public ActionResult Create(ClienteViewModel viewModel)
        {
            try
            {
                ClienteEntity input = ViewModelToEntity.MapCliente(viewModel);

                AdicionarClienteUseCase useCase = new AdicionarClienteUseCase(
                    _geoLocalizacaoService,
                    _tokenService,
                    _clienteRepository,
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
                    return RedirectToAction("Index", "Cliente");
                }

                long id = Int64.Parse(valor);
    
                BuscarClienteUseCase useCase = new BuscarClienteUseCase(_clienteRepository, id);
    
                ClienteEntity entity = useCase.Execute();
    
                ClienteViewModel viewModel = EntityToViewModel.MapCliente(entity);
    
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
        public ActionResult Edit(ClienteViewModel viewModel)
        {
            try
            {
                ClienteEntity input = ViewModelToEntity.MapCliente(viewModel);
    
                AtualizarClienteUseCase useCase = new AtualizarClienteUseCase(_clienteRepository, input);
                useCase.Execute();
    
                return RedirectToAction("Index", "Cliente");
            }
            catch (Exception e)
            {
                ViewData["MensagemErro"] = e.Message;
                return View();
            }            
        }
    }
}

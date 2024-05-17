using System.Security.Claims;
using System.Security.Policy;
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
        private readonly ISolicitacaoRepository _solicitacaoRepository;
        private readonly ITokenService _tokenService;
        private readonly IGeoLocalizacaoService _geoLocalizacaoService;
        private readonly IAuthRepository _repo;

        public ClienteController(ICryptoService cryptoService, IUsuarioRepository usuarioRepository, IClienteRepository clienteRepository, ISolicitacaoRepository solicitacaoRepository, ITokenService tokenService, IAuthRepository repo, IGeoLocalizacaoService geoLocalizacaoService)
        {
            _cryptoService = cryptoService;
            _usuarioRepository = usuarioRepository;
            _clienteRepository = clienteRepository;
            _solicitacaoRepository = solicitacaoRepository;
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

            if (TipoUsuarioAutenticado != TipoUsuario.Cliente)
            {
                return RedirectToAction("Index","Home");
            }

            ExisteClienteComIdUsuarioUseCase existeClienteComIdUsuario = new ExisteClienteComIdUsuarioUseCase(_clienteRepository,  IdUsuarioAutenticado);
            bool existeCliente = existeClienteComIdUsuario.Execute();
            if(!existeCliente)
            {
                return RedirectToAction("Create","Cliente");
            }

            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync();
                return RedirectToAction("Login","Cliente");
            }

            if (TipoUsuarioAutenticado != TipoUsuario.Cliente)
            {
                return RedirectToAction("Index","Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpViewModel vmSignUp)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync();
                return RedirectToAction("Login","Cliente");
            }

            if (TipoUsuarioAutenticado != TipoUsuario.Cliente)
            {
                return RedirectToAction("Index","Home");
            }
            
            if(vmSignUp == null)
                return View();

            UsuarioEntity usuario = new UsuarioEntity();
            usuario.Login = vmSignUp.Login;
            usuario.Password1 = vmSignUp.Password;
            usuario.Password2 = vmSignUp.Password2;
            usuario.Email = vmSignUp.Email;
            usuario.Celular = vmSignUp.Celular;
            usuario.TipoDeUsuario = TipoUsuario.Cliente;

            try
            {
                AdicionarUsuarioUseCase useCase = new AdicionarUsuarioUseCase(_cryptoService,_usuarioRepository,usuario);
                useCase.Execute();

                return RedirectToAction("Index", "Cliente");
            }
            catch(Exception e)
            {
                ViewData["MensagemErro"] = e.Message;
                return View();
            }
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
                if (TipoUsuarioAutenticado != TipoUsuario.Cliente)
                {
                    return RedirectToAction("Index","Home");
                }

                ViewData["FormAction"] = "Create";

                ClienteViewModel viewModel = new ClienteViewModel();
                viewModel.IdUsuario = IdUsuarioAutenticado;
    
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
                if (TipoUsuarioAutenticado != TipoUsuario.Cliente)
                {
                    return RedirectToAction("Index","Home");
                }
                
                ExisteClienteComIdUsuarioUseCase existeClienteComIdUsuario = new ExisteClienteComIdUsuarioUseCase(_clienteRepository,  IdUsuarioAutenticado);
                bool existeCliente = existeClienteComIdUsuario.Execute();
                if(!existeCliente)
                {
                    return RedirectToAction("Create","Cliente");
                }

                ViewData["FormAction"] = "Edit";
                
                BuscarClienteUseCase useCase = new BuscarClienteUseCase(_clienteRepository, IdUsuarioAutenticado);
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
                if (TipoUsuarioAutenticado != TipoUsuario.Cliente)
                {
                    return RedirectToAction("Index","Home");
                }
                
                ExisteClienteComIdUsuarioUseCase existeClienteComIdUsuario = new ExisteClienteComIdUsuarioUseCase(_clienteRepository,  IdUsuarioAutenticado);
                bool existeCliente = existeClienteComIdUsuario.Execute();
                if(!existeCliente)
                {
                    return RedirectToAction("Create","Cliente");
                }

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
    
        [HttpGet]
        public ActionResult Solicitacoes()
        {
            try
            {
                if (TipoUsuarioAutenticado != TipoUsuario.Cliente)
                {
                    return RedirectToAction("Index","Home");
                }

                ExisteClienteComIdUsuarioUseCase existeClienteComIdUsuario = new ExisteClienteComIdUsuarioUseCase(_clienteRepository,  IdUsuarioAutenticado);
                bool existeCliente = existeClienteComIdUsuario.Execute();
                if(!existeCliente)
                {
                    return RedirectToAction("Create","Cliente");
                }

                SolicitacoesEmAbertoUseCase solicitacoesEmAberto = new SolicitacoesEmAbertoUseCase(_solicitacaoRepository, IdUsuarioAutenticado);
                IEnumerable<SolicitacaoModel> lst = solicitacoesEmAberto.Execute();

                SolicitacoesViewModel solicitacoes = new SolicitacoesViewModel(lst);

                return View(solicitacoes);
            }
            catch(Exception e)
            {
                ViewData["MensagemErro"] = e.Message;
                return View();
            }
        }
    
    }
}

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

            if (TipoUsuarioAutenticado != TipoUsuario.Colaborador)
            {
                return RedirectToAction("Index","Home");
            }
            
            ExisteColaboradorComIdUsuarioUseCase existeColaboradorComIdUsuario = new ExisteColaboradorComIdUsuarioUseCase(_colaboradorRepository,  IdUsuarioAutenticado);
            bool existeCliente = existeColaboradorComIdUsuario.Execute();
            if(!existeCliente)
            {
                return RedirectToAction("Create","Colaborador");
            }

            IndexClienteViewModel viewModel = new IndexClienteViewModel();
            viewModel.TITULO_PAGINA = "Painel do Colaborador";

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            SignUpViewModel viewModel = new SignUpViewModel();
            viewModel.TITULO_PAGINA = "Inscrição do Colaborador";
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SignUp(SignUpViewModel viewModel)
        {
            
            if(viewModel == null)
                return View();

            UsuarioEntity usuario = new UsuarioEntity();
            usuario.Login = viewModel.Login;
            usuario.Password1 = viewModel.Password;
            usuario.Password2 = viewModel.Password2;
            usuario.Email = viewModel.Email;
            usuario.Celular = viewModel.Celular;
            usuario.TipoDeUsuario = TipoUsuario.Colaborador;

            try
            {
                AdicionarUsuarioUseCase useCase = new AdicionarUsuarioUseCase(_cryptoService,_usuarioRepository,usuario);
                useCase.Execute();

                return RedirectToAction("Index", "Colaborador");
            }
            catch(Exception e)
            {
                viewModel.MENSAGEM_ERRO = e.Message;
                return View(viewModel);
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync();
                return RedirectToAction("Login","Colaborador");
            }
            
            LoginViewModel viewModel = new LoginViewModel();
            viewModel.TITULO_PAGINA = "Login do Colaborador";

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel viewModel)
        {
            try
            {
                LoginDTO dto = new LoginDTO
                {
                    Login = viewModel.Login,
                    Password1 = viewModel.Password
                };

                EfetuarLoginUseCase useCase = new EfetuarLoginUseCase(_cryptoService, _tokenService, _repo, dto, TipoUsuario.Colaborador);
                List<Claim> lstClaims = useCase.Execute();

                var authProperties = new AuthenticationProperties
                {
                    RedirectUri = "/Colaborador/Index",
                    ExpiresUtc = viewModel.RememberMe ? DateTime.UtcNow.AddYears(2) : DateTime.UtcNow.AddHours(1),
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
                viewModel = new LoginViewModel
                {
                    TITULO_PAGINA = "Login do Colaborador",
                    MENSAGEM_ERRO = e.Message
                };
                return View(viewModel);
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
                if (TipoUsuarioAutenticado != TipoUsuario.Colaborador)
                {
                    return RedirectToAction("Index","Home");
                }

                ColaboradorViewModel viewModel = new ColaboradorViewModel
                {
                    IdUsuario = IdUsuarioAutenticado,
                    FORM_ACTION = "Create",
                    TITULO_PAGINA = "Cadastro de Colaborador"
                };

                return View(viewModel);
            }
            catch (Exception e)
            {
                ColaboradorViewModel viewModel = new ColaboradorViewModel
                {
                    MENSAGEM_ERRO = e.Message
                };
                return View(viewModel);
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
                viewModel.MENSAGEM_ERRO = e.Message;
                return View(viewModel);
            }
        }

        [HttpGet]
        public ActionResult Edit()
        {
            try
            {
                if (TipoUsuarioAutenticado != TipoUsuario.Colaborador)
                {
                    return RedirectToAction("Index","Home");
                }
                
                ExisteColaboradorComIdUsuarioUseCase existeColaboradorComIdUsuario = new ExisteColaboradorComIdUsuarioUseCase(_colaboradorRepository,  IdUsuarioAutenticado);
                bool existeCliente = existeColaboradorComIdUsuario.Execute();
                if(!existeCliente)
                {
                    return RedirectToAction("Create","Colaborador");
                }
    
                BuscarColaboradorByUsuarioUseCase useCase = new BuscarColaboradorByUsuarioUseCase(_colaboradorRepository, IdUsuarioAutenticado);
                ColaboradorEntity entity = useCase.Execute();
                ColaboradorViewModel viewModel = EntityToViewModel.MapColaborador(entity);
                viewModel.TITULO_PAGINA = "Dados do colaborador";
                viewModel.FORM_ACTION = "Edit";
    
                return View(viewModel);
            }
            catch (Exception e)
            {                
                ColaboradorViewModel viewModel = new ColaboradorViewModel();
                viewModel.MENSAGEM_ERRO = e.Message;
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ColaboradorViewModel viewModel)
        {
            try
            {
                if (TipoUsuarioAutenticado != TipoUsuario.Colaborador)
                {
                    return RedirectToAction("Index","Home");
                }
                
                ExisteColaboradorComIdUsuarioUseCase existeColaboradorComIdUsuario = new ExisteColaboradorComIdUsuarioUseCase(_colaboradorRepository,  IdUsuarioAutenticado);
                bool existeCliente = existeColaboradorComIdUsuario.Execute();
                if(!existeCliente)
                {
                    return RedirectToAction("Create","Colaborador");
                }
                
                ColaboradorEntity input = ViewModelToEntity.MapColaborador(viewModel);
    
                AtualizarColaboradorUseCase useCase = new AtualizarColaboradorUseCase(_colaboradorRepository, input);
                useCase.Execute();
    
                return RedirectToAction("Index", "Colaborador");
            }
            catch (Exception e)
            {
                viewModel.TITULO_PAGINA = "Dados do colaborador";
                viewModel.MENSAGEM_ERRO = e.Message;
                return View(viewModel);
            }            
        }
    }
}

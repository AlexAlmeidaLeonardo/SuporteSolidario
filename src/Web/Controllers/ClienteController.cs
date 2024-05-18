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
using SuporteSolidarioBusiness.Infrastructure.MySQL;

namespace SuporteSolidario.Controllers
{
    public class ClienteController : BaseController
    {
        private readonly ICryptoService _cryptoService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly ISolicitacaoRepository _solicitacaoRepository;
        private readonly IServicoRepository _servicoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ITokenService _tokenService;
        private readonly IGeoLocalizacaoService _geoLocalizacaoService;
        private readonly IAuthRepository _repo;

        public ClienteController(ICryptoService cryptoService, IUsuarioRepository usuarioRepository, IClienteRepository clienteRepository, ISolicitacaoRepository solicitacaoRepository, IServicoRepository servicoRepository, ICategoriaRepository categoriaRepository, ITokenService tokenService, IAuthRepository repo, IGeoLocalizacaoService geoLocalizacaoService)
        {
            _cryptoService = cryptoService;
            _usuarioRepository = usuarioRepository;
            _clienteRepository = clienteRepository;
            _solicitacaoRepository = solicitacaoRepository;
            _servicoRepository = servicoRepository;
            _categoriaRepository = categoriaRepository;
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

            IndexClienteViewModel viewModel = new IndexClienteViewModel();
            viewModel.TITULO_PAGINA = "Painel do Cliente";

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            SignUpViewModel viewModel = new SignUpViewModel();
            viewModel.TITULO_PAGINA = "Inscrição do Cliente";
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
            usuario.TipoDeUsuario = TipoUsuario.Cliente;

            try
            {
                AdicionarUsuarioUseCase useCase = new AdicionarUsuarioUseCase(_cryptoService,_usuarioRepository,usuario);
                useCase.Execute();

                return RedirectToAction("Index", "Cliente");
            }
            catch(Exception e)
            {
                viewModel = new SignUpViewModel
                {
                    TITULO_PAGINA = "Inscrição do Cliente",
                    MENSAGEM_ERRO = e.Message
                };
                return View(viewModel);
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

            LoginViewModel viewModel = new LoginViewModel();
            viewModel.TITULO_PAGINA = "Login do Cliente";

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

                EfetuarLoginUseCase useCase = new EfetuarLoginUseCase(_cryptoService, _tokenService, _repo, dto, TipoUsuario.Cliente);
                List<Claim> lstClaims = useCase.Execute();

                var authProperties = new AuthenticationProperties
                {
                    RedirectUri = "/Cliente/Index",
                    ExpiresUtc = viewModel.RememberMe ? DateTime.UtcNow.AddYears(2) : DateTime.UtcNow.AddHours(1),
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
                viewModel = new LoginViewModel
                {
                    TITULO_PAGINA = "Login do Cliente",
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
            ClienteViewModel viewModel = new ClienteViewModel();
            try
            {
                if (TipoUsuarioAutenticado != TipoUsuario.Cliente)
                {
                    return RedirectToAction("Index","Home");
                }

                viewModel.IdUsuario = IdUsuarioAutenticado;
                viewModel.FORM_ACTION = "Create";
                viewModel.TITULO_PAGINA = "Cadastro de Cliente";
            }
            catch (Exception e)
            {                
                viewModel.MENSAGEM_ERRO = e.Message;
            }
            return View(viewModel);
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
                viewModel.MENSAGEM_ERRO = e.Message;
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

                BuscarClienteUseCase useCase = new BuscarClienteUseCase(_clienteRepository, IdUsuarioAutenticado);
                ClienteEntity entity = useCase.Execute();
                ClienteViewModel viewModel = EntityToViewModel.MapCliente(entity);
                viewModel.FORM_ACTION = "Edit";
    
                return View(viewModel);
            }
            catch (Exception e)
            {
                ClienteViewModel viewModel = new ClienteViewModel();
                viewModel.MENSAGEM_ERRO = e.Message;
                return View(viewModel);
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
                viewModel.MENSAGEM_ERRO = e.Message;
                return View(viewModel);
            }            
        }
    
        [HttpGet]
        public ActionResult SolicitacaoCategoria()
        {
            BuscarTodasCategoriaUseCase buscarTodasCategoria = new BuscarTodasCategoriaUseCase(_categoriaRepository);
            IEnumerable<CategoriaEntity> lst = buscarTodasCategoria.Execute();
            /*
            lst.Add(new CategoriaModel{ Id = 1, Descricao = "Jardinagem" });
            lst.Add(new CategoriaModel{ Id = 2, Descricao = "Reforma" });
            lst.Add(new CategoriaModel{ Id = 3, Descricao = "Alimentação" });
            lst.Add(new CategoriaModel{ Id = 4, Descricao = "Enfermagem" });
            lst.Add(new CategoriaModel{ Id = 5, Descricao = "Entregas" });
            lst.Add(new CategoriaModel{ Id = 6, Descricao = "Manutenção" });
            */

            SolicitacaoCategoriaViewModel viewModel = new SolicitacaoCategoriaViewModel( lst );
            viewModel.TITULO_PAGINA = "Qual categoria de serviço você precisa?";
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SolicitacaoCategoria(long IdCategoria)
        {
            if(IdCategoria > 0)
                return RedirectToAction("SolicitacaoServico", new { IdCategoria });

            SolicitacaoCategoriaViewModel viewModel = new SolicitacaoCategoriaViewModel(new List<CategoriaEntity>());
            viewModel.TITULO_PAGINA = "Categoria selecionada: " + IdCategoria;
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult SolicitacaoServico(long idCategoria)
        {
            BuscarServicosPorCategoriaUseCase buscarServicosPorCategoria = new BuscarServicosPorCategoriaUseCase(_servicoRepository, idCategoria);
            IEnumerable<ServicoEntity> lst = buscarServicosPorCategoria.Execute();
            /*
            lst.Add(new ServicoModel{ Id = 1, IdCategoria = idCategoria, Descricao = "Regar as rosas" });
            lst.Add(new ServicoModel{ Id = 2, IdCategoria = idCategoria, Descricao = "Regar as margaridas" });
            lst.Add(new ServicoModel{ Id = 3, IdCategoria = idCategoria, Descricao = "Regar as tulipas" });
            lst.Add(new ServicoModel{ Id = 4, IdCategoria = idCategoria, Descricao = "Regar as orquideas" });
            lst.Add(new ServicoModel{ Id = 5, IdCategoria = idCategoria, Descricao = "Regar os girassois" });
            lst.Add(new ServicoModel{ Id = 6, IdCategoria = idCategoria, Descricao = "Regar as papoulas" });
            */

            SolicitacaoServicoViewModel viewModel = new SolicitacaoServicoViewModel( lst );
            viewModel.TITULO_PAGINA = "Qual tipo de serviço você precisa?";
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SolicitacaoServicoPost(long IdServico)
        {
            if(IdServico > 0)
                return RedirectToAction("Solicitacao", new { IdServico });

            return View();
        }

        [HttpGet]
        public ActionResult Solicitacao(long IdServico)
        {
            BuscarServicoUseCase buscarServico = new BuscarServicoUseCase(_servicoRepository, IdServico);
            ServicoEntity servico = buscarServico.Execute();
    
            BuscarCategoriaUseCase buscarCategoria = new BuscarCategoriaUseCase(_categoriaRepository, servico.IdCategoria);
            CategoriaEntity categoria = buscarCategoria.Execute();

            SolicitacaoViewModel viewModel = new SolicitacaoViewModel();
            viewModel.IdServico = IdServico;
            viewModel.DescricaoCategoria = categoria.Descricao;
            viewModel.DescricaoServico = servico.Descricao;
            viewModel.TITULO_PAGINA = "Explique melhor que serviço você deseja";

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Solicitacao(SolicitacaoViewModel viewModel)
        {
            BuscarClienteByUsuarioUseCase buscarClienteByUsuario = new BuscarClienteByUsuarioUseCase(_clienteRepository, IdUsuarioAutenticado);
            ClienteEntity cliente = buscarClienteByUsuario.Execute();

            if(cliente == null)
            {
                return RedirectToAction("Index","Cliente");
            }

            SolicitacaoEntity solicitacaoEntity = new SolicitacaoEntity
            {
                IdServico = viewModel.IdServico,
                IdCliente = cliente.Id,
                DataServico = viewModel.DataServico,
                Detalhes = viewModel.Detalhes
            };

            AdicionarSolicitacaoUseCase adicionarSolicitacao = new AdicionarSolicitacaoUseCase(_solicitacaoRepository, _clienteRepository, solicitacaoEntity);
            SolicitacaoEntity retorno = adicionarSolicitacao.Execute();

            if( retorno == null )
            {
                viewModel.MENSAGEM_ERRO = "Não foi possível criar a solicitação. Tente novamente mais tarde.";
                return View(viewModel);
            }

            long IdSolicitacao = retorno.Id;

            return RedirectToAction("SolicitacaoConcluida", new { IdSolicitacao });
        }

        [HttpGet]
        public ActionResult SolicitacaoConcluida(long IdSolicitacao)
        {
            try
            {
                BuscarSolicitacaoUseCase buscarSolicitacao = new BuscarSolicitacaoUseCase(_solicitacaoRepository, IdSolicitacao);
                SolicitacaoEntity solicitacao = buscarSolicitacao.Execute();
    
                BuscarServicoUseCase buscarServico = new BuscarServicoUseCase(_servicoRepository, solicitacao.IdServico);
                ServicoEntity servico = buscarServico.Execute();
    
                BuscarCategoriaUseCase buscarCategoria = new BuscarCategoriaUseCase(_categoriaRepository, servico.IdCategoria);
                CategoriaEntity categoria = buscarCategoria.Execute();
    
                SolicitacaoConcluidaViewModel viewModel = new SolicitacaoConcluidaViewModel();
                viewModel.IdSolicitacao = solicitacao.Id;
                viewModel.DescricaoCategoria = categoria.Descricao;
                viewModel.DescricaoServico = servico.Descricao;
                viewModel.DataServico = solicitacao.DataServico;
                viewModel.Data = solicitacao.Data;
                viewModel.TITULO_PAGINA = "Solicitação Concluída!";
    
                return View(viewModel);
            }
            catch (Exception e)
            {
                SolicitacaoConcluidaViewModel viewModel = new SolicitacaoConcluidaViewModel();
                viewModel.TITULO_PAGINA = "Não foi possível concluir a solicitação.";
                viewModel.MENSAGEM_ERRO = e.Message;    
                return View(viewModel);
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
                SolicitacoesViewModel solicitacoes = new SolicitacoesViewModel(null);
                solicitacoes.MENSAGEM_ERRO = e.Message;
                return View(solicitacoes);
            }
        }
    
    }
}

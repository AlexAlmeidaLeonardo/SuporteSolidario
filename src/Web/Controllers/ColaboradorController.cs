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

namespace SuporteSolidario.Controllers;

public class ColaboradorController : BaseController
{
    private readonly ICryptoService _cryptoService;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IColaboradorRepository _colaboradorRepository;
    private readonly IColaboradorServicoRepository _colaboradorServicoRepository;
    private readonly ISolicitacaoRepository _solicitacaoRepository;
    private readonly IAtendimentoRepository _atendimentoRepository;
    private readonly IAtendimentoMensagemRepository _atendimentoMensagemRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly ITokenService _tokenService;
    private readonly IGeoLocalizacaoService _geoLocalizacaoService;
    private readonly IAuthRepository _repo;

    public ColaboradorController(ICryptoService cryptoService, IColaboradorRepository colaboradorRepository, IUsuarioRepository usuarioRepository, IColaboradorServicoRepository colaboradorServicoRepository, ISolicitacaoRepository solicitacaoRepository, IAtendimentoRepository atendimentoRepository, IAtendimentoMensagemRepository atendimentoMensagemRepository, IClienteRepository clienteRepository, ITokenService tokenService, IAuthRepository repo, IGeoLocalizacaoService geoLocalizacaoService)
    {
        _cryptoService = cryptoService;
        _usuarioRepository = usuarioRepository;
        _colaboradorRepository = colaboradorRepository;
        _colaboradorServicoRepository = colaboradorServicoRepository;
        _solicitacaoRepository = solicitacaoRepository;
        _atendimentoRepository = atendimentoRepository;
        _atendimentoMensagemRepository = atendimentoMensagemRepository;
        _clienteRepository = clienteRepository;
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
            return RedirectToAction("Login", "Colaborador");
        }

        if (TipoUsuarioAutenticado != TipoUsuario.Colaborador)
        {
            return RedirectToAction("Index", "Home");
        }

        BuscarColaboradorByUsuarioUseCase buscarColaboradorByUsuario = new BuscarColaboradorByUsuarioUseCase(_colaboradorRepository, IdUsuarioAutenticado);
        ColaboradorEntity colaborador = buscarColaboradorByUsuario.Execute();

        if (colaborador == null)
        {
            return RedirectToAction("Create", "Colaborador");
        }

        BuscarSolicitacoesPorProximidadeUseCase buscarSolicitacoesPorProximidade = new BuscarSolicitacoesPorProximidadeUseCase(_solicitacaoRepository, colaborador.Id, 100);

        IndexColaboradorViewModel viewModel = new IndexColaboradorViewModel();
        viewModel.TITULO_PAGINA = "Painel do Colaborador";
        viewModel.listaServicosEmAberto = buscarSolicitacoesPorProximidade.Execute();

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

        if (viewModel == null)
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
            AdicionarUsuarioUseCase useCase = new AdicionarUsuarioUseCase(_cryptoService, _usuarioRepository, usuario);
            useCase.Execute();

            return RedirectToAction("Index", "Colaborador");
        }
        catch (Exception e)
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
            return RedirectToAction("Login", "Colaborador");
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
        catch (Exception e)
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
                return RedirectToAction("Index", "Home");
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
        catch (Exception e)
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
                return RedirectToAction("Index", "Home");
            }

            ExisteColaboradorComIdUsuarioUseCase existeColaboradorComIdUsuario = new ExisteColaboradorComIdUsuarioUseCase(_colaboradorRepository, IdUsuarioAutenticado);
            bool existeCliente = existeColaboradorComIdUsuario.Execute();
            if (!existeCliente)
            {
                return RedirectToAction("Create", "Colaborador");
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
                return RedirectToAction("Index", "Home");
            }

            ExisteColaboradorComIdUsuarioUseCase existeColaboradorComIdUsuario = new ExisteColaboradorComIdUsuarioUseCase(_colaboradorRepository, IdUsuarioAutenticado);
            bool existeCliente = existeColaboradorComIdUsuario.Execute();
            if (!existeCliente)
            {
                return RedirectToAction("Create", "Colaborador");
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

    [HttpGet]
    public ActionResult Servicos()
    {
        try
        {
            if (TipoUsuarioAutenticado != TipoUsuario.Colaborador)
            {
                return RedirectToAction("Index", "Home");
            }

            ExisteColaboradorComIdUsuarioUseCase existeColaboradorComIdUsuario = new ExisteColaboradorComIdUsuarioUseCase(_colaboradorRepository, IdUsuarioAutenticado);
            bool existeCliente = existeColaboradorComIdUsuario.Execute();
            if (!existeCliente)
            {
                return RedirectToAction("Create", "Colaborador");
            }

            BuscarColaboradorByUsuarioUseCase useCase = new BuscarColaboradorByUsuarioUseCase(_colaboradorRepository, IdUsuarioAutenticado);
            ColaboradorEntity entity = useCase.Execute();

            BuscarServicosDoColaboradorUseCase buscarServicosDoColaborador = new BuscarServicosDoColaboradorUseCase(_colaboradorServicoRepository, entity.Id);
            IEnumerable<ColaboradorServicoDTO> lstServicosPrestados = buscarServicosDoColaborador.Execute();

            BuscarServicosNaoPrestadosUseCase buscarServicosNaoPrestados = new BuscarServicosNaoPrestadosUseCase(_colaboradorServicoRepository, entity.Id, null);
            IEnumerable<ServicoDTO> lstServicosNaoPrestados = buscarServicosNaoPrestados.Execute();

            ColaboradorPorServicoViewModel viewModel = new ColaboradorPorServicoViewModel();
            viewModel.TITULO_PAGINA = "Seleção de serviços";
            viewModel.listColaboradorServicos = lstServicosPrestados;
            viewModel.listServicos = lstServicosNaoPrestados;

            return View(viewModel);

        }
        catch (Exception e)
        {
            return View();
        }
    }

    [HttpPost]
    public ActionResult Servicos(ColaboradorPorServicoViewModel viewModel)
    {
        try
        {
            if (TipoUsuarioAutenticado != TipoUsuario.Colaborador)
            {
                return RedirectToAction("Index", "Home");
            }

            ExisteColaboradorComIdUsuarioUseCase existeColaboradorComIdUsuario = new ExisteColaboradorComIdUsuarioUseCase(_colaboradorRepository, IdUsuarioAutenticado);
            bool existeCliente = existeColaboradorComIdUsuario.Execute();
            if (!existeCliente)
            {
                return RedirectToAction("Create", "Colaborador");
            }

            BuscarColaboradorByUsuarioUseCase useCase = new BuscarColaboradorByUsuarioUseCase(_colaboradorRepository, IdUsuarioAutenticado);
            ColaboradorEntity entity = useCase.Execute();

            BuscarServicosDoColaboradorUseCase buscarServicosDoColaborador = new BuscarServicosDoColaboradorUseCase(_colaboradorServicoRepository, entity.Id);
            IEnumerable<ColaboradorServicoDTO> lstServicosPrestados = buscarServicosDoColaborador.Execute();

            BuscarServicosNaoPrestadosUseCase buscarServicosNaoPrestados = new BuscarServicosNaoPrestadosUseCase(_colaboradorServicoRepository, entity.Id, viewModel.PesquisaDescricao);
            IEnumerable<ServicoDTO> lstServicosNaoPrestados = buscarServicosNaoPrestados.Execute();

            viewModel.TITULO_PAGINA = "Seleção de serviços";
            viewModel.listColaboradorServicos = lstServicosPrestados;
            viewModel.listServicos = lstServicosNaoPrestados;

            return View(viewModel);

        }
        catch (Exception e)
        {
            return View();
        }
    }

    [HttpGet]
    public ActionResult AdicionaServico(long id)
    {
        BuscarColaboradorByUsuarioUseCase buscarColaboradorByUsuario = new BuscarColaboradorByUsuarioUseCase(_colaboradorRepository, IdUsuarioAutenticado);
        ColaboradorEntity entity = buscarColaboradorByUsuario.Execute();

        ColaboradorServicoEntity input = new ColaboradorServicoEntity();
        input.IdServico = id;
        input.IdColaborador = entity.Id;

        AdicionarColaboradorServicoUseCase adicionarColaborador = new AdicionarColaboradorServicoUseCase(_colaboradorServicoRepository, input);
        adicionarColaborador.Execute();

        return RedirectToAction("Servicos", "Colaborador");
    }

    [HttpGet]
    public ActionResult RemoveServico(long id)
    {
        RemoverColaboradorServicoUseCase useCase = new RemoverColaboradorServicoUseCase(_colaboradorServicoRepository, id);
        useCase.Execute();

        return RedirectToAction("Servicos", "Colaborador");
    }

    [HttpGet]
    public ActionResult DetalhesSolicitacao(long idSolicitacao)
    {
        BuscarDetalhesSolicitacaoUseCase buscarDetalhesSolicitacao = new BuscarDetalhesSolicitacaoUseCase(_colaboradorRepository, idSolicitacao);
        DetalhesSolicitacaoViewModel detalhesSolicitacaoViewModel = new DetalhesSolicitacaoViewModel();

        DetalhesSolicitacaoDTO detalhesSolicitacao = buscarDetalhesSolicitacao.Execute();

        detalhesSolicitacaoViewModel.IdSolicitacao = detalhesSolicitacao.Id;
        detalhesSolicitacaoViewModel.NomeCliente = detalhesSolicitacao.Nome;
        detalhesSolicitacaoViewModel.DistanciaEmKm = detalhesSolicitacao.Distancia;
        detalhesSolicitacaoViewModel.DescricaoServico = detalhesSolicitacao.Servico;
        detalhesSolicitacaoViewModel.DescricaoCategoria = detalhesSolicitacao.Categoria;
        detalhesSolicitacaoViewModel.DataServico = detalhesSolicitacao.DataServico;
        detalhesSolicitacaoViewModel.TITULO_PAGINA = "Detalhes da Solicitação";

        return View(detalhesSolicitacaoViewModel);
    }

    [HttpGet]
    public ActionResult RecusarSolicitacao(long idSolicitacao)
    {
        RecusarAtendimentoUseCase useCase = new RecusarAtendimentoUseCase(_atendimentoRepository, idSolicitacao);
        bool retorno = useCase.Execute();

        if (!retorno)
        {
            return RedirectToAction("Index", "Home");
        }


        return RedirectToAction("DetalhesSolicitacao", "Colaborador", new {idSolicitacao = idSolicitacao});
    }

    [HttpGet]
    public ActionResult AceitarSolicitacao(long idSolicitacao)
    {
        BuscarColaboradorByUsuarioUseCase buscarColaboradorByUsuario = new BuscarColaboradorByUsuarioUseCase(_colaboradorRepository, IdUsuarioAutenticado);
        ColaboradorEntity colaborador = buscarColaboradorByUsuario.Execute();

        AtendimentoEntity atendimento = new AtendimentoEntity();
        atendimento.IdSolicitacao = idSolicitacao;
        atendimento.IdColaborador = colaborador.Id;

        // Cria-se um atendimento
        AdicionarAtendimentoUseCase adicionarAtendimento = new AdicionarAtendimentoUseCase(_atendimentoRepository, atendimento);
        AtendimentoEntity retorno = adicionarAtendimento.Execute();

        if (retorno != null)
        {
            return RedirectToAction("MensagemColaborador", "Colaborador", new {idAtendimento = retorno.Id});
        }

        return DetalhesSolicitacao(idSolicitacao);
    }

    [HttpGet]
    public ActionResult MensagemColaborador(long idAtendimento)
    {
        MensagemColaboradorViewModel mensagemColaborador = new MensagemColaboradorViewModel();
        mensagemColaborador.TITULO_PAGINA = "Conversa com o cliente";
        mensagemColaborador.IdAtendimento = idAtendimento;

        BuscarMensagensUseCase buscarMensagens = new BuscarMensagensUseCase(_atendimentoMensagemRepository, idAtendimento);
        mensagemColaborador.ListaMensagens = buscarMensagens.Execute();

        BuscarClientePorAtendimentoUseCase buscarClientePorAtendimento = new BuscarClientePorAtendimentoUseCase(_clienteRepository, idAtendimento);
        ClienteDTO cliente = buscarClientePorAtendimento.Execute();
        mensagemColaborador.NomeCliente = cliente.Nome + " " + cliente.Sobrenome;

        return View(mensagemColaborador);
    }

    [HttpPost]
    public ActionResult MensagemColaborador(MensagemColaboradorViewModel mensagemColaborador)
    {
        if (string.IsNullOrEmpty(mensagemColaborador.Mensagem))
        {
            mensagemColaborador.TITULO_PAGINA = "Conversa com o cliente";
            mensagemColaborador.MENSAGEM_ERRO = "Não é permitidos mensagens em branco";

            BuscarMensagensUseCase buscarMensagens = new BuscarMensagensUseCase(_atendimentoMensagemRepository, mensagemColaborador.IdAtendimento);
            mensagemColaborador.ListaMensagens = buscarMensagens.Execute();

            BuscarClientePorAtendimentoUseCase buscarClientePorAtendimento = new BuscarClientePorAtendimentoUseCase(_clienteRepository, mensagemColaborador.IdAtendimento);
            ClienteDTO cliente = buscarClientePorAtendimento.Execute();
            mensagemColaborador.NomeCliente = cliente.Nome + " " + cliente.Sobrenome;
            
            return View(mensagemColaborador);
        }

        AtendimentoMensagemEntity mensagem = new AtendimentoMensagemEntity();
        mensagem.IdAtendimento = mensagemColaborador.IdAtendimento;
        mensagem.Mensagem = mensagemColaborador.Mensagem;
        
        AdicionarMensagemColaboradorUseCase adicionarMensagemColaborador = new AdicionarMensagemColaboradorUseCase(_atendimentoMensagemRepository, mensagem);
        mensagem = adicionarMensagemColaborador.Execute();

        if (mensagem == null)
        {
            mensagemColaborador.MENSAGEM_ERRO = "Não foi possível enviar a mensagem.\nTente novamente";
            return View(mensagemColaborador);
        }

   
        return RedirectToAction("Index", "Colaborador");
    }

}
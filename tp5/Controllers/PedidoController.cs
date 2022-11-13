namespace tp5.Controllers;

public class PedidoController : Controller
{
    private readonly ILogger<PedidoController> _logger;
    private readonly IRepositorioPedido _repositorio;
    private readonly IRepositorio<Cadete> _repositorioCadete;
    private readonly IRepositorio<Cliente> _repositorioCliente;
    private readonly IMapper _mapper;

    public PedidoController(ILogger<PedidoController> logger, IRepositorioPedido repositorio,
        IRepositorio<Cadete> repositorioCadete, IRepositorio<Cliente> repositorioCliente, IMapper mapper)
    {
        _logger = logger;
        _repositorio = repositorio;
        _repositorioCadete = repositorioCadete;
        _repositorioCliente = repositorioCliente;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var pedidos = _repositorio.BuscarTodos();
        var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);
        return View(pedidosViewModel);
    }

    [HttpGet]
    public IActionResult BuscarTodosPorCliente(int id)
    {
        var pedidos = _repositorio.BuscarTodosPorCliente(id);
        var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);
        return View(pedidosViewModel);
    }

    [HttpGet]
    public IActionResult BuscarTodosPorCadete(int id)
    {
        var pedidos = _repositorio.BuscarTodosPorCadete(id);
        var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);
        return View(pedidosViewModel);
    }

    [HttpGet]
    public IActionResult AltaPedido()
    {
        var cadetes = _repositorioCadete.BuscarTodos();
        var clientes = _repositorioCliente.BuscarTodos();
        var cadetesViewModel = _mapper.Map<List<CadeteViewModel>>(cadetes);
        var clientesViewModel = _mapper.Map<List<ClienteViewModel>>(clientes);
        var pedidoAltaViewModel = new PedidoAltaViewModel(cadetesViewModel, clientesViewModel);
        return View(pedidoAltaViewModel);
    }

    [HttpPost]
    public IActionResult AltaPedido(PedidoViewModel pedidoViewModel)
    {
        if (ModelState.IsValid)
        {
            var pedido = _mapper.Map<Pedido>(pedidoViewModel);
            _repositorio.Insertar(pedido);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarPedido(int id)
    {
        var pedido = _repositorio.BuscarPorId(id);
        
        var cadetes = _repositorioCadete.BuscarTodos();
        var cadetesViewModel = _mapper.Map<List<CadeteViewModel>>(cadetes);
        
        var pedidoModificadoViewModel = _mapper.Map<PedidoModificadoViewModel>(pedido);
        pedidoModificadoViewModel.Cadetes = cadetesViewModel;
        
        return View(pedidoModificadoViewModel);
    }

    [HttpPost]
    public IActionResult ModificarPedido(PedidoViewModel pedidoViewModel)
    {
        if (ModelState.IsValid)
        {
            var pedido = _mapper.Map<Pedido>(pedidoViewModel);
            _repositorio.Actualizar(pedido);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult BajaPedido(int id)
    {
        _repositorio.Eliminar(id);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
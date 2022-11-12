namespace tp5.Controllers;

public class PedidoController : Controller
{
    private readonly ILogger<PedidoController> _logger;
    private readonly IRepositorio<Pedido> _repositorio;
    private readonly IMapper _mapper;

    public PedidoController(ILogger<PedidoController> logger, IRepositorio<Pedido> repositorio, IMapper mapper)
    {
        _logger = logger;
        _repositorio = repositorio;
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
    public IActionResult AltaPedido()
    {
        return View("AltaPedido");
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
        if (pedido is null) return RedirectToAction("Index");
        var pedidoViewModel = _mapper.Map<PedidoViewModel>(pedido);
        return View(pedidoViewModel);
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
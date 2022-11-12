namespace tp5.Controllers;

public class PedidoController : Controller
{
    private const string PedidosArchivoPath = "C:\\Taller\\Pedidos.csv";

    private static int _id;

    private readonly ILogger<PedidoController> _logger;
    private readonly IMapper _mapper;

    public PedidoController(ILogger<PedidoController> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var pedidos = LeerArchivoPedido(PedidosArchivoPath);
        var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);
        _id = pedidos.Count;
        return View(pedidosViewModel);
    }

    [HttpGet]
    public IActionResult AltaPedido()
    {
        return View("AltaPedido");
    }

    [HttpPost]
    public IActionResult AltaPedido(Pedido pedido)
    {
        pedido.Id = _id++;
        AgregarEntidad(PedidosArchivoPath, pedido);
        // return View("Index", _cadetes);
        // Response.Redirect("/Cadete");
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarPedido(int id)
    {
        var pedidos = LeerArchivoPedido(PedidosArchivoPath);
        var pedidoBuscado = pedidos.Find(x => x.Id == id);
        var pedidoViewModel = _mapper.Map<PedidoViewModel>(pedidoBuscado);
        return View(pedidoViewModel);
    }

    [HttpPost]
    public IActionResult ModificarPedido(Pedido pedido)
    {
        Console.WriteLine(pedido.Id);
        var pedidos = LeerArchivoPedido(PedidosArchivoPath);
        var indicePedido = pedidos.FindIndex(x => x.Id == pedido.Id);
        pedidos[indicePedido] = pedido;
        EliminarArchivo(PedidosArchivoPath);
        CrearArchivo(PedidosArchivoPath, pedidos);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult BajaPedido(int id)
    {
        var pedidos = LeerArchivoPedido(PedidosArchivoPath);
        pedidos.RemoveAll(x => x.Id == id);
        EliminarArchivo(PedidosArchivoPath);
        CrearArchivo(PedidosArchivoPath, pedidos);
        return RedirectToAction("Index");
    }
}
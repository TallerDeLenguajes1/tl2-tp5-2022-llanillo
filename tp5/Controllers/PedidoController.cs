namespace tp5.Controllers;

public class PedidoController : Controller
{
    private const string PedidosArchivoPath = "C:\\Taller\\Pedidos.csv";

    private readonly ILogger<PedidoController> _logger;
    private readonly IMapper _mapper;

    private static int _id;

    public PedidoController(ILogger<PedidoController> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        // var cadetes = LeerCsvCadetes(CadetesArchivoPath);
        // var cadetesViewModel = _mapper.Map<List<CadeteViewModel>>(cadetes);
        // _id = cadetes.Count;
        return View("Index");
    }

    [HttpGet]
    public IActionResult AltaPedido()
    {
        var pedidos = LeerArchivoCadete(PedidosArchivoPath);
        var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);
        _id = pedidos.Count;
        return View(pedidosViewModel);
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
    public IActionResult BajaPedido(int id)
    {
        // var cadetes = LeerCsvCadetes(CadetesArchivoPath);
        // cadetes.RemoveAll(x => x.Id == id);
        // EliminarCsv(CadetesArchivoPath);
        // CrearArchivo(CadetesArchivoPath, cadetes);
        return RedirectToAction("Index");
    }
}
namespace tp5.Controllers;

public class CadeteController : Controller
{
    private const string CadetesArchivoPath = "C:\\Taller\\Cadeteria.csv";
    
    private readonly ILogger<CadeteController> _logger;
    private readonly IMapper _mapper;
    
    private static int _id;

    public CadeteController(ILogger<CadeteController> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        var cadetes = LeerCsvCadetes(CadetesArchivoPath);
        var cadetesViewModel = _mapper.Map<List<CadeteViewModel>>(cadetes);
        _id = cadetes.Count;
        return View(cadetesViewModel);
    }

    [HttpGet]
    public IActionResult AltaCadete()
    {
        return View("AltaCadete");
    }

    [HttpPost]
    public IActionResult AltaCadete(Cadete cadete)
    {
        cadete.Id = _id++;
        AgregarCadeteCsv(CadetesArchivoPath, cadete);
        // return View("Index", _cadetes);
        // Response.Redirect("/Cadete");
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult BajaCadete(int id)
    {
        var cadetes = LeerCsvCadetes(CadetesArchivoPath);
        cadetes.RemoveAll(x => x.Id == id);
        EliminarCsv(CadetesArchivoPath);
        CrearCadetesCsv(CadetesArchivoPath, cadetes);
        return RedirectToAction("Index");
    }
}
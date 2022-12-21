namespace tp5.Controllers;

public class HomeController : Controller
{
    private const string SessionId = "Id";
    private const string SessionNombre = "Nombre";
    private const string SessionUsuario = "Usuario";
    private const string SessionRol = "Rol";

    private readonly ILogger<HomeController> _logger;
    private readonly IRepositorioUsuario _repositorio;
    private readonly IMapper _mapper;

    public HomeController(ILogger<HomeController> logger, IMapper mapper, IRepositorioUsuario repositorio)
    {
        _logger = logger;
        _mapper = mapper;
        _repositorio = repositorio;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }


    [HttpPost]
    public IActionResult InicioSesion(InicioSesionViewModel inicioSesionViewModel)
    {
        var usuario = _mapper.Map<Usuario>(inicioSesionViewModel);
        usuario = _repositorio.Verificar(usuario);

        if (usuario is null) return RedirectToAction("Index");
        if (usuario.Rol == Rol.Ninguno) return RedirectToAction("Index");

        HttpContext.Session.SetInt32(SessionId, usuario.Id);
        HttpContext.Session.SetString(SessionNombre, usuario.Nombre);
        HttpContext.Session.SetString(SessionUsuario, usuario.NombreUsuario);
        HttpContext.Session.SetInt32(SessionRol, (int)usuario.Rol);

        return RedirectToAction("Index", "Pedido");
    }

    [HttpGet]
    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
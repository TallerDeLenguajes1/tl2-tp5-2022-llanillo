using tp5.ViewModels.Home;
using tp5.ViewModels.Usuario.General;

namespace tp5.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;
    private readonly RepositorioUsuarioBase _repositorioUsuarioBase;

    public HomeController(ILogger<HomeController> logger, IMapper mapper, RepositorioUsuarioBase repositorioUsuarioBase)
    {
        _logger = logger;
        _mapper = mapper;
        _repositorioUsuarioBase = repositorioUsuarioBase;
    }

    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            var inicioViewModel = new HomeViewModel();
            var usuarios = _repositorioUsuarioBase.BuscarTodos();
            var usuariosViewModel = _mapper.Map<List<UsuarioViewModel>>(usuarios);
            inicioViewModel.UsuarioViewModels = usuariosViewModel;
            return View(inicioViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError("Error al acceder al Index {Error}", e.Message);
            return View("Error");
        }
    }


    [HttpPost]
    public IActionResult InicioSesion(HomeViewModel homeViewModel)
    {
        try
        {
            var usuario = _mapper.Map<Usuario>(homeViewModel.LoginViewModel);
            usuario = _repositorioUsuarioBase.Verificar(usuario);

            if (usuario is null || usuario.Rol == Rol.Ninguno) return RedirectToAction("Index");

            HttpContext.Session.SetInt32(SessionId, usuario.Id);
            HttpContext.Session.SetString(SessionNombre, usuario.Nombre);
            HttpContext.Session.SetString(SessionUsuario, usuario.NombreUsuario);
            HttpContext.Session.SetInt32(SessionRol, (int)usuario.Rol);

            return RedirectToAction("Index", "Pedido");
        }
        catch (Exception e)
        {
            _logger.LogError("Error al acceder el Inicio Sesión {Error}", e.Message);
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult CerrarSesion()
    {
        try
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            _logger.LogError("Error al acceder el Cerrar Sesión {Error}", e.Message);
            return View("Error");
        }
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
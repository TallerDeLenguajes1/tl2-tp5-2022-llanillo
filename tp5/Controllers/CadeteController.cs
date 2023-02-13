namespace tp5.Controllers;

public class CadeteController : Controller
{
    private readonly ILogger<CadeteController> _logger;
    private readonly IMapper _mapper;
    private readonly RepositorioPedidoBase _repositorioPedidoBase;
    private readonly RepositorioUsuarioBase _repositorioUsuarioBase;

    public CadeteController(ILogger<CadeteController> logger, IMapper mapper, RepositorioUsuarioBase repositorioUsuarioBase,
        RepositorioPedidoBase repositorioPedidoBase)
    {
        _logger = logger;
        _mapper = mapper;
        _repositorioUsuarioBase = repositorioUsuarioBase;
        _repositorioPedidoBase = repositorioPedidoBase;
    }

    public IActionResult Index()
    {
        try
        {
            if (HttpContext.Session.GetInt32(SessionRol) != (int)Rol.Administrador)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }

            var cadetes = _repositorioUsuarioBase.BuscarTodosPorRol(Rol.Cadete);
            var cadetesViewModel = _mapper.Map<List<UsuarioViewModel>>(cadetes);
            return View(cadetesViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError("Error al acceder al Index {Error}", e.Message);
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult AltaCadete()
    {
        try
        {
            if (HttpContext.Session.GetInt32(SessionRol) == (int)Rol.Administrador)
            {
                return View("AltaCadete");
            }
            
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");

        }
        catch (Exception e)
        {
            _logger.LogError("Error al acceder el Alta de Cadete {Error}", e.Message);
            return View("Error");
        }
    }

    /*
     * Alta cadete llamada una vez le da a "Enviar" a través del formulario
     */
    [HttpPost]
    public IActionResult AltaCadete(UsuarioViewModel cadeteViewModel)
    {
        try
        {
            if (HttpContext.Session.GetInt32(SessionRol) != (int)Rol.Administrador)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var cadete = _mapper.Map<Usuario>(cadeteViewModel);
                _repositorioUsuarioBase.Insertar(cadete);
            }
            else
            {
                var errores = ModelState.Values.SelectMany(x => x.Errors);
                Console.WriteLine(errores);
            }

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            _logger.LogError("Error al acceder el Alta de Cadete {Error}", e.Message);
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult ModificarCadete(int id)
    {
        try
        {
            if (HttpContext.Session.GetInt32(SessionRol) != (int)Rol.Administrador)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }

            var cadete = _repositorioUsuarioBase.BuscarPorId(id);
            if (cadete is null) return RedirectToAction("Index");
            var cadeteViewModel = _mapper.Map<UsuarioViewModel>(cadete);
            return View(cadeteViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError("Error al acceder el Modificar Cadete {Error}", e.Message);
            return View("Error");
        }
    }

    [HttpPost]
    public IActionResult ModificarCadete(UsuarioModificadoViewModel cadeteViewModel)
    {
        try
        {
            if (HttpContext.Session.GetInt32(SessionRol) != (int)Rol.Administrador)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var cadete = _mapper.Map<Usuario>(cadeteViewModel);
                _repositorioUsuarioBase.Actualizar(cadete);
            }
            else
            {
                var errores = ModelState.Values.SelectMany(x => x.Errors);
                Console.WriteLine(errores);
            }

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            _logger.LogError("Error al acceder el Modificar Cadete {Error}", e.Message);
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult Informacion(int id)
    {
        try
        {
            if (HttpContext.Session.GetInt32(SessionRol) != (int)Rol.Administrador)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }

            var pedidosDelCadete = _repositorioPedidoBase.BuscarTodosPorUsuarioYRol(id, Rol.Cadete);
            var pedidosDelCadeteViewModel = _mapper.Map<List<PedidoViewModel>>(pedidosDelCadete);

            var enviosCadete = new EnviosCadeteViewModel
            {
                CantidadEnvios = pedidosDelCadeteViewModel.Count,
                MontoGanado = pedidosDelCadeteViewModel.Sum(x => x.Estado == "Entregado" ? 300 : 0)
            };

            var informacionCadete = new InformacionCadete
            {
                EnviosCadeteViewModel = enviosCadete,
                PedidoViewModels = pedidosDelCadeteViewModel
            };

            return View(informacionCadete);
        }
        catch (Exception e)
        {
            _logger.LogError("Error al acceder el Modificar Cadete {Error}", e.Message);
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult BajaCadete(int id)
    {
        try
        {
            if (HttpContext.Session.GetInt32(SessionRol) != (int)Rol.Administrador)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }

            _repositorioUsuarioBase.Eliminar(id);
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            _logger.LogError("Error al acceder el Baja Cadete {Error}", e.Message);
            return View("Error");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        if (HttpContext.Session.GetInt32(SessionRol) == (int)Rol.Administrador)
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
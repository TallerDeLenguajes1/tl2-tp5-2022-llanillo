namespace tp5.Controllers;

public class PedidoController : Controller
{
    private readonly ILogger<PedidoController> _logger;
    private readonly IMapper _mapper;
    private readonly RepositorioPedidoBase _repositorioPedidoBase;
    private readonly RepositorioUsuarioBase _repositorioUsuarioBase;

    public PedidoController(ILogger<PedidoController> logger, RepositorioPedidoBase repositorioPedidoBase,
        RepositorioUsuarioBase repositorioUsuarioBase, IMapper mapper)
    {
        _logger = logger;
        _repositorioPedidoBase = repositorioPedidoBase;
        _repositorioUsuarioBase = repositorioUsuarioBase;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            var sesionRol = (Rol)HttpContext.Session.GetInt32(SessionRol);

            switch (sesionRol)
            {
                case Rol.Administrador:
                {
                    var pedidos = _repositorioPedidoBase.BuscarTodos();
                    var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);

                    foreach (var pedido in pedidosViewModel)
                    {
                        var clienteId = int.Parse(pedido.Cliente);
                        var idCadete = int.Parse(pedido.Cadete);

                        pedido.Cliente = _repositorioUsuarioBase.BuscarPorId(clienteId)?.Nombre;
                        pedido.Cadete = _repositorioUsuarioBase.BuscarPorId(idCadete)?.Nombre;
                    }

                    return View(pedidosViewModel);
                }
                case Rol.Cadete:
                {
                    var idCadete = (int)HttpContext.Session.GetInt32(SessionId);
                    var pedidos = _repositorioPedidoBase.BuscarTodosPorUsuarioYRol(idCadete, Rol.Cadete);
                    var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);

                    foreach (var pedido in pedidosViewModel)
                    {
                        var idCliente = int.Parse(pedido.Cliente);
                        pedido.Cliente = _repositorioUsuarioBase.BuscarPorId(idCliente)?.Nombre;
                    }

                    return View(pedidosViewModel);
                }
                case Rol.Cliente:
                {
                    var idCliente = (int)HttpContext.Session.GetInt32(SessionId);
                    var pedidos = _repositorioPedidoBase.BuscarTodosPorUsuarioYRol(idCliente, Rol.Cliente);
                    var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);

                    foreach (var pedido in pedidosViewModel)
                    {
                        var idCadete = int.Parse(pedido.Cadete);
                        pedido.Cadete = _repositorioUsuarioBase.BuscarPorId(idCadete)?.Nombre;
                    }

                    return View(pedidosViewModel);
                }
                case Rol.Ninguno:
                    return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
        catch (Exception e)
        {
            _logger.LogError("Error al acceder el Index {Error}", e.Message);
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult AltaPedido()
    {
        try
        {
            var sesionRol = HttpContext.Session.GetInt32(SessionRol);
            if (sesionRol != (int)Rol.Administrador && sesionRol != (int)Rol.Cliente)
                return RedirectToAction("Index", "Home");

            var cadetes = _repositorioUsuarioBase.BuscarTodosPorRol(Rol.Cadete);
            var clientes = _repositorioUsuarioBase.BuscarTodosPorRol(Rol.Cliente);
            var cadetesViewModel = _mapper.Map<List<UsuarioViewModel>>(cadetes);
            var clientesViewModel = _mapper.Map<List<UsuarioViewModel>>(clientes);
            var pedidoAltaViewModel = new PedidoAltaViewModel(cadetesViewModel, clientesViewModel);
            return View(pedidoAltaViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError("Error al acceder el Alta Pedido {Error}", e.Message);
            return View("Error");
        }
    }

    [HttpPost]
    public IActionResult AltaPedido(PedidoAltaViewModel pedidoViewModel)
    {
        try
        {
            var sesionRol = HttpContext.Session.GetInt32(SessionRol);
            if (sesionRol != (int)Rol.Administrador && sesionRol != (int)Rol.Cliente)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var pedido = _mapper.Map<Pedido>(pedidoViewModel);
                _repositorioPedidoBase.Insertar(pedido);
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
            _logger.LogError("Error al acceder el Alta Pedido {Error}", e.Message);
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult ModificarPedido(int id)
    {
        try
        {
            if (!HttpContext.Session.IsAvailable) return RedirectToAction("Index", "Home");

            var pedido = _repositorioPedidoBase.BuscarPorId(id);

            var cadetes = _repositorioUsuarioBase.BuscarTodosPorRol(Rol.Cadete);
            var cadetesViewModel = _mapper.Map<List<UsuarioViewModel>>(cadetes);

            var clientes = _repositorioUsuarioBase.BuscarTodosPorRol(Rol.Cliente);
            var clientesViewModel = _mapper.Map<List<UsuarioViewModel>>(clientes);

            var pedidoModificadoViewModel = _mapper.Map<PedidoModificadoViewModel>(pedido);
            pedidoModificadoViewModel.Cadetes = cadetesViewModel;
            pedidoModificadoViewModel.Clientes = clientesViewModel;

            return View(pedidoModificadoViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError("Error al acceder el Modificar Pedido {Error}", e.Message);
            return View("Error");
        }
    }

    [HttpPost]
    public IActionResult ModificarPedido(PedidoViewModel pedidoViewModel)
    {
        try
        {
            if (!HttpContext.Session.IsAvailable) return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var pedido = _mapper.Map<Pedido>(pedidoViewModel);
                _repositorioPedidoBase.Actualizar(pedido);
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
            _logger.LogError("Error al acceder el Modificar Pedido {Error}", e.Message);
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult BajaPedido(int id)
    {
        try
        {
            var sesionRol = HttpContext.Session.GetInt32(SessionRol);
            if (sesionRol != (int)Rol.Administrador && sesionRol != (int)Rol.Cadete)
                return RedirectToAction("Index", "Home");

            _repositorioPedidoBase.Eliminar(id);
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            _logger.LogError("Error al acceder el Baja Pedido {Error}", e.Message);
            return View("Error");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var sesionRol = HttpContext.Session.GetInt32(SessionRol);
        if (sesionRol != (int)Rol.Administrador && sesionRol != (int)Rol.Cadete)
            return RedirectToAction("Index", "Home");

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
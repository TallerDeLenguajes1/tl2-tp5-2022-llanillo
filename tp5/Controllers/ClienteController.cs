namespace tp5.Controllers;

public class ClienteController : Controller
{
    private readonly ILogger<ClienteController> _logger;
    private readonly IRepositorio<Cliente> _repositorio;
    private readonly IMapper _mapper;

    public ClienteController(ILogger<ClienteController> logger, IRepositorio<Cliente> repositorio, IMapper mapper)
    {
        _logger = logger;
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        var clientes = _repositorio.BuscarTodos();
        var clientesViewModel = _mapper.Map<List<ClienteViewModel>>(clientes);
        return View(clientesViewModel);
    }

    [HttpGet]
    public IActionResult AltaCliente()
    {
        return View("AltaCliente");
    }

    [HttpPost]
    public IActionResult AltaCliente(ClienteViewModel clienteViewModel)
    {
        if (ModelState.IsValid)
        {
            var cliente = _mapper.Map<Cliente>(clienteViewModel);
            _repositorio.Insertar(cliente);
        }
        else
        {
            var errores = ModelState.Values.SelectMany(x => x.Errors);
            Console.WriteLine(errores);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarCliente(int id)
    {
        var cliente = _repositorio.BuscarPorId(id);
        if (cliente is null) return RedirectToAction("Index");
        var clienteViewModel = _mapper.Map<ClienteViewModel>(cliente);
        return View(clienteViewModel);
    }

    [HttpPost]
    public IActionResult ModificarCliente(ClienteViewModel clienteViewModel)
    {
        if (ModelState.IsValid)
        {
            var cliente = _mapper.Map<Cliente>(clienteViewModel);
            _repositorio.Actualizar(cliente);
        }
        else
        {
            var errores = ModelState.Values.SelectMany(x => x.Errors);
            Console.WriteLine(errores);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult BajaCliente(int id)
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
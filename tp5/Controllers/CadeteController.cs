using Microsoft.AspNetCore.Mvc;
using tp5.Models;
namespace tp5.Controllers;

public class CadeteController : Controller
{
    private readonly ILogger<CadeteController> _logger;

    private static readonly List<Cadete> Cadetes = new();
    private static int _id;


    public CadeteController(ILogger<CadeteController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(Cadetes);
    }

    [HttpGet]
    public IActionResult AltaCadete()
    {
        return View();
    }

    [HttpPost]
    public void AltaCadeteExito(Cadete cadete)
    {
        cadete.Id = ++_id;
        Cadetes.Add(cadete);
        // return View("Index", _cadetes);
        Response.Redirect("/Cadete");
    }

    [HttpGet]
    public void BajaCadete(int id)
    {
        Cadetes.RemoveAll(x => x.Id == id);
        Response.Redirect("/Cadete");
    }
}
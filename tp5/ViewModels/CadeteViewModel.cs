namespace tp5.ViewModels;

public class CadeteViewModel
{
    public int Id { get; set; }

    [Required] [Display(Name = "Nombre")] public string Nombre { get; set; }

    [Required]
    [Display(Name = "Dirreción")]
    public string Direccion { get; set; }

    [Required]
    [Display(Name = "Teléfono")]
    public string Telefono { get; set; }

    public int Cadeteria { get; set; }

    public CadeteViewModel()
    {
    }

    public CadeteViewModel(int id, string nombre, string direccion, string telefono, int cadeteria)
    {
        Id = id;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        Cadeteria = cadeteria;
    }
}
namespace tp5.ViewModels;

public class CadeteViewModel
{
    [Required] public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; }

    [Required]
    [StringLength(300)]
    [Display(Name = "Dirreción")]
    public string Direccion { get; set; }

    [Required]
    [Phone]
    [Display(Name = "Teléfono")]
    public int Telefono { get; set; }

    [Required] public int IdCadeteria { get; set; }

    public CadeteViewModel() { }

    public CadeteViewModel(int id, string nombre, string direccion, int telefono, int idCadeteria)
    {
        Id = id;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        IdCadeteria = idCadeteria;
    }
}
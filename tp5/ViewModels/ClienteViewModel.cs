namespace tp5.ViewModels;

public class ClienteViewModel
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

    public ClienteViewModel() { }

    public ClienteViewModel(int id, string nombre, string direccion, int telefono)
    {
        Id = id;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
    }
}
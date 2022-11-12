namespace tp5.ViewModels.Cadete;

public class CadeteViewModel
{
    public CadeteViewModel(string nombre, string direccion, long telefono)
    {
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
    }

    [Required] public int Id { get; set; }

    [Required] [StringLength(100)] public string Nombre { get; set; }

    [Required] [StringLength(300)] public string Direccion { get; set; }

    [Required] [Phone] public long Telefono { get; set; }
}
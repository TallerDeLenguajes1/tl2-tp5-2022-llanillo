namespace tp5.ViewModels.Cadete;

public class CadeteViewModel
{
    public int Id { get; set; }

    public string Nombre { get; set; }   

    public string Direccion { get; set; }

    public long Telefono { get; set; }

    public CadeteViewModel (string nombre, string direccion, long telefono){
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
    }
}
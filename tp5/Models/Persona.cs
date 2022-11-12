namespace tp5.Models;

public abstract class Persona
{
    public Persona()
    {
    }

    public Persona(int id, string nombre, string direccion, long telefono)
    {
        Id = id;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
    }

    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Direccion { get; set; }

    public long Telefono { get; set; }
}
namespace tp5.Models;

public abstract class Persona
{
    public Persona()
    {
    }

    public Persona(int id, string nombre, string direccion, string telefono)
    {
        Id = id;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
    }

    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Direccion { get; set; }

    public string Telefono { get; set; }

    public override string ToString()
    {
        return "Código: " + Id + " Nombre: " + Nombre + " Dirección: " + Direccion + " Teléfono: " + Telefono;
    }
}
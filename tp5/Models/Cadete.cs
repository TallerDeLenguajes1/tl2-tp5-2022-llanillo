namespace tp5.Models;

public class Cadete : Persona
{
    public Cadete()
    {
    }

    public Cadete(int id, string nombre, string direccion, long telefono) : base(id, nombre, direccion, telefono)
    {
    }

    public override string ToString()
    {
        return Id + " " + Nombre + " " + Direccion + " " + Telefono;
    }
}
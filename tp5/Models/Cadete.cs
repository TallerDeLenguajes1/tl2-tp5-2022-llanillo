namespace tp5.Models;

public class Cadete : Persona
{
    public Cadete() { }

    public Cadete(string nombre, string direccion, long telefono) : base(nombre, direccion, telefono){ }

    public override string ToString()
    {
        return Id + " " + Nombre + " " + Direccion + " " + Telefono;
    }
}


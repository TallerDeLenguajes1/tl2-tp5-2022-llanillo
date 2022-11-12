namespace tp5.Models;

public class Cadete : Persona
{
    public int? Cadeteria { get; set; }

    public Cadete()
    {
    }

    public Cadete(int id, string nombre, string direccion, int telefono, int? cadeteria) : base(id, nombre, direccion,
        telefono)
    {
        Cadeteria = cadeteria;
    }
}
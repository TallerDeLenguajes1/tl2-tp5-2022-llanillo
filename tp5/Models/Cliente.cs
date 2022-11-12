namespace tp5.Models;

public class Cliente : Persona
{
    public Cliente()
    {
    }

    public Cliente(int id, string nombre, string direccion, int telefono) : base(id, nombre, direccion, telefono)
    {
    }
}
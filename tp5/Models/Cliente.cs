namespace tp5.Models;

public class Cliente : Persona
{
    public Cliente()
    {
    }

    public Cliente(int id, string nombre, string nombreUsuario, string clave, Rol rol, string direccion,
        string telefono) : base(id, nombre, nombreUsuario, clave, rol, direccion, telefono)
    {
    }

}
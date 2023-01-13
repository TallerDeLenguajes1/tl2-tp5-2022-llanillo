namespace tp5.Models;

public class Cadete : Persona
{
    public Cadete()
    {
    }

    public Cadete(int id, string nombre, string nombreUsuario, string clave, Rol rol, string direccion, string telefono)
        : base(id, nombre, nombreUsuario, clave, rol, direccion, telefono)
    {
    }
}
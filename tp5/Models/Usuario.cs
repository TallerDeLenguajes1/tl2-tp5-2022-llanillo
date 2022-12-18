namespace tp5.Models;

public class Usuario
{
    public int Id { get; set; }
    
    public string Nombre { get; set; }
    
    public string NombreUsuario { get; set; }
    
    public string Clave { get; set; }
    
    public Rol Rol { get; set; }

    public Usuario()
    {
        
    }
    
    public Usuario(int id, string nombre, string nombreUsuario, string clave, Rol rol)
    {
        Id = id;
        Nombre = nombre;
        NombreUsuario = nombreUsuario;
        Clave = clave;
        Rol = rol;
    }
}
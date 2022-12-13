namespace tp5.Models;

public class Usuario
{
    public int Id { get; set; }
    
    public string Nombre { get; set; }
    
    public string NombreUsuario { get; set; }
    
    public string Contrasena { get; set; }
    
    public string Rol { get; set; }

    public Usuario()
    {
        
    }
    
    public Usuario(int id, string nombre, string nombreUsuario, string contrasena, string rol)
    {
        Id = id;
        Nombre = nombre;
        NombreUsuario = nombreUsuario;
        Contrasena = contrasena;
        Rol = rol;
    }
}
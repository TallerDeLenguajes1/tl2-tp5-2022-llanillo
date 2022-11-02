namespace tp5.Models{

public abstract class Persona{
    
    public int Id { get; set; }

    public string Nombre { get; set; }   

    public string Direccion { get; set; }

    public long Telefono { get; set; }

    public Persona() { }

    public Persona (string nombre, string direccion, long telefono){
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
    }
}
}

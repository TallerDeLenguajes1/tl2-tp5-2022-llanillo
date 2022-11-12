namespace tp5.Models;

public class Cadeteria
{
    public Cadeteria()
    {
    }

    public Cadeteria(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }

    public int Id { get; set; }
    public string Nombre { get; set; }
}
namespace tp5.Models;

public class Pedido{

    public int Id { get; set; }

    public string Observacion { get; set; }

    public Estado Estado { get; set; }
    
    public Cliente Cliente { get; set; }

    public Pedido(int id, string observacion, Estado estado, Cliente cliente)
    {
        Id = id;
        Observacion = observacion;
        Estado = estado;
        Cliente = cliente;
    }

    public override string? ToString()
    {
        return "Número: " + Id + " Cliente: " + Cliente.Nombre + " Estado: " + (Estado);
    }
}
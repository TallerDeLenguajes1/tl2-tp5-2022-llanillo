﻿namespace tp5.ViewModels.Pedido;

public class PedidoViewModel
{
    [Required]
    public int Id { get; set; }

    [Required][StringLength(200)]
    public string Observacion { get; set; }   

    [Required]
    public Estado Estado { get; set; }
    
    public Cliente Cliente { get; set; }

    public PedidoViewModel(int id, string observacion, Estado estado)
    {
        Id = id;
        Observacion = observacion;
        Estado = estado;
        Cliente = new Cliente("", "", 0);
    }
}
﻿namespace tp5.ViewModels;

public class PedidoAltaViewModel
{
    public readonly List<CadeteViewModel> Cadetes;

    public readonly List<ClienteViewModel> Clientes;

    public PedidoAltaViewModel(List<CadeteViewModel> cadetes, List<ClienteViewModel> clientes)
    {
        Cadetes = cadetes;
        Clientes = clientes;
    }

    [StringLength(200)]
    [Display(Name = "Observacion")]
    public string Observacion { get; set; }

    [Required]
    [StringLength(30)]
    [Display(Name = "Estado")]
    public string Estado { get; set; }

    [Required] public int Cliente { get; set; }
    [Required] public int Cadete { get; set; }
}
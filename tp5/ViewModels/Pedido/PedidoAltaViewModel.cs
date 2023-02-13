using tp5.ViewModels.Usuario.General;

namespace tp5.ViewModels.Pedido;

public class PedidoAltaViewModel
{
    public readonly List<UsuarioViewModel> Cadetes;

    public readonly List<UsuarioViewModel> Clientes;

    public PedidoAltaViewModel()
    {
    }

    public PedidoAltaViewModel(List<UsuarioViewModel> cadetes, List<UsuarioViewModel> clientes)
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

    public int Cliente { get; set; } = -1;
    public int Cadete { get; set; } = -1;
}
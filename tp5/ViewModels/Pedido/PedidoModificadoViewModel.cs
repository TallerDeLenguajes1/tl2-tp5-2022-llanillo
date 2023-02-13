using tp5.ViewModels.Usuario.General;

namespace tp5.ViewModels.Pedido;

public class PedidoModificadoViewModel
{
    [Required] public int Id { get; set; }

    [Required]
    [StringLength(200)]
    [Display(Name = "Observacion")]
    public string Observacion { get; set; }

    [StringLength(30)]
    [Display(Name = "Estado")]
    public string Estado { get; set; }

    public int Cadete { get; set; }
    public int Cliente { get; set; }

    public List<UsuarioViewModel> Cadetes { get; set; }
    public List<UsuarioViewModel> Clientes { get; set; }
}
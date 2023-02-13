namespace tp5.ViewModels.Usuario.Cadete;

public class InformacionCadete
{
    [Required] [Display(Name = "Envios")] public EnviosCadeteViewModel EnviosCadeteViewModel { get; set; }

    [Required] [Display(Name = "Envios")] public List<PedidoViewModel> PedidoViewModels { get; set; }
}
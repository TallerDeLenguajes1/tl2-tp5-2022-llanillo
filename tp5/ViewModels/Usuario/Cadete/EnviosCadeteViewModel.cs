namespace tp5.ViewModels.Usuario.Cadete;

public class EnviosCadeteViewModel
{
    [Required]
    [Display(Name = "Monto_Ganado")]
    public long MontoGanado { get; set; }

    [Required] [Display(Name = "Estado")] public long CantidadEnvios { get; set; }
}
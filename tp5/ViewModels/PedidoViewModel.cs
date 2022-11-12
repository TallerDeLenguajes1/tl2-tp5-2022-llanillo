namespace tp5.ViewModels;

public class PedidoViewModel
{
    [Required] public int Id { get; set; }

    [Required]
    [StringLength(200)]
    [Display(Name = "Observación")]
    public string Observacion { get; set; }

    [Required]
    [StringLength(30)]
    [Display(Name = "Estado")]
    public string Estado { get; set; }

    [Required] public int Cliente { get; set; }
    [Required] public int Cadete { get; set; }
    
    public PedidoViewModel() { }

    public PedidoViewModel(int id, string observacion, string estado, int cliente, int cadete)
    {
        Id = id;
        Observacion = observacion;
        Estado = estado;
        Cliente = cliente;
        Cadete = cadete;
    }
}
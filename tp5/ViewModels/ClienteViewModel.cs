namespace tp5.ViewModels;

public class ClienteViewModel
{
    [Required] public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; }

    [Required]
    [StringLength(300)]
    [Display(Name = "Dirreción")]
    public string Direccion { get; set; }

    [Required]
    [Display(Name = "Teléfono")]
    public string Telefono { get; set; }
}
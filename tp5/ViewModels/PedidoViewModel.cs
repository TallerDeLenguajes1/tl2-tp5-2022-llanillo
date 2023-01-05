﻿namespace tp5.ViewModels;

public class PedidoViewModel
{
    [Required] public int Id { get; set; }

    [Required]
    [StringLength(200)]
    [Display(Name = "Observacion")]
    public string Observacion { get; set; }

    [Required]
    [StringLength(30)]
    [Display(Name = "Estado")]
    public string Estado { get; set; }

    [Required] public int Cliente { get; set; }
    [Required] public int Cadete { get; set; }
    
    [Required] public String ClientStr { get; set; }
    [Required] public String CadeteStr { get; set; }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SysCafeteria.EntidadDeNegocio;

namespace SysCafeteria.EntidadDeNegocio;

public class Producto
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Código es obligatorio")]
    [StringLength(15, ErrorMessage = "Maximo 15 caracteres")]
    public string Codigo { get; set; } = null!;

    [Required(ErrorMessage = "Nombre es obligatorio")]
    [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "Descripción es obligatorio")]
    [Column(TypeName = "text")]
    public string Descripcion { get; set; } = null!;

    [Required(ErrorMessage = "Precio de venta es obligatorio")]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal PrecioVenta { get; set; }

    [ForeignKey("Categoria")]
    [Required(ErrorMessage = "Categoria es obligatoria")]
    [Display(Name = "Categoria")]
    public int IdCategoria { get; set; }
    public Categoria Categoria {  get; set; }

    [NotMapped]
    public int Top_Aux { get; set; }

    public List<Pedido>? Pedidos { get; set; }
}

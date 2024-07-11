using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SysCafeteria.EntidadesDeNegocio;

namespace SysCafeteria.EntidadDeNegocio;

public class Categoria
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nombre es obligatorio")]
    [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
    public string Nombre { get; set; } = null!;

    [NotMapped]
    public int Top_Aux { get; set; }
    public List<Producto>? Productos { get; set; }
}

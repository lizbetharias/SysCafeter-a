using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SysCafeteria.EntidadDeNegocio;
using SysCafeteria.EntidadesDeNegocio;

namespace SysCafeteria.AccesoADatos;

public partial class BdContext : DbContext
{
    public BdContext()
    {
    }

    public BdContext(DbContextOptions<BdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Pedido> Pedido { get; set; }

    public virtual DbSet<Producto> Producto { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("workstation id=DBCofee2024.mssql.somee.com;packet size=4096;user id=Arias34_SQLLogin_1;pwd=klfy31imqq;data source=DBCofee2024.mssql.somee.com;persist security info=False;initial catalog=DBCofee2024;TrustServerCertificate=True");

}

using Microsoft.EntityFrameworkCore;
using SysCafeteria.EntidadDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysCafeteria.AccesoADatos
{
    public class ProductoDAL
    {
        public static async Task<int> CrearAsync(Producto pProducto)
        {
            int result = 0;
            using (var bdContexto = new BdContext())
            {
                bdContexto.Add(pProducto);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Producto pProducto)
        {
            int result = 0;
            using (var bdContexto = new BdContext())
            {
                var producto = await bdContexto.Producto.FirstOrDefaultAsync(s => s.Id == pProducto.Id);
                producto.Codigo = pProducto.Codigo;
                producto.Nombre = pProducto.Nombre;
                producto.Descripcion = pProducto.Descripcion;
                producto.PrecioVenta = pProducto.PrecioVenta;
                producto.IdCategoria = pProducto.IdCategoria;
                bdContexto.Update(producto);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Producto pProducto)
        {
            int result = 0;
            using (var bdContexto = new BdContext())
            {
                var producto = await bdContexto.Producto.FirstOrDefaultAsync(s => s.Id == pProducto.Id);
                bdContexto.Producto.Remove(producto);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Producto> ObtenerPorIdAsync(Producto pProducto)
        {
            var producto = new Producto();
            using (var bdContexto = new BdContext())
            {
                producto = await bdContexto.Producto.FirstOrDefaultAsync(s => s.Id == pProducto.Id);
            }
            return producto;
        }

        public static async Task<List<Producto>> ObtenerTodosAsync()
        {
            var productos = new List<Producto>();
            using (var bdContexto = new BdContext())
            {
                productos = await bdContexto.Producto.ToListAsync();
            }
            return productos;
        }

        internal static IQueryable<Producto> QuerySelect(IQueryable<Producto> pQuery, Producto pProducto)
        {
            if (pProducto.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pProducto.Id);
            if (!string.IsNullOrWhiteSpace(pProducto.Codigo))
                pQuery = pQuery.Where(s => s.Codigo.Contains(pProducto.Codigo));
            if (!string.IsNullOrWhiteSpace(pProducto.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pProducto.Nombre));
            if (!string.IsNullOrWhiteSpace(pProducto.Descripcion))
                pQuery = pQuery.Where(s => s.Descripcion.Contains(pProducto.Descripcion));
            if (pProducto.PrecioVenta > 0)
                pQuery = pQuery.Where(s => s.PrecioVenta == pProducto.PrecioVenta);
            if (pProducto.IdCategoria > 0)
                pQuery = pQuery.Where(s => s.IdCategoria == pProducto.IdCategoria);
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pProducto.Top_Aux > 0)
                pQuery = pQuery.Take(pProducto.Top_Aux).AsQueryable();
            return pQuery;
        }

        public static async Task<List<Producto>> BuscarAsync(Producto pProducto)
        {
            var productos = new List<Producto>();
            using (var bdContexto = new BdContext())
            {
                var select = bdContexto.Producto.AsQueryable();
                select = QuerySelect(select, pProducto);
                productos = await select.ToListAsync();
            }
            return productos;
        }
    }
}

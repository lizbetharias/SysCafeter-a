using Microsoft.EntityFrameworkCore;
using SysCafeteria.EntidadDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysCafeteria.AccesoADatos
{
    public class PedidoDAL
    {
        public static async Task<int> CrearAsync(Pedido pPedido)
        {
            int result = 0;
            using (var bdContexto = new BdContext())
            {
                bdContexto.Add(pPedido);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Pedido pPedido)
        {
            int result = 0;
            using (var bdContexto = new BdContext())
            {
                var pedido = await bdContexto.Pedido.FirstOrDefaultAsync(s => s.Id == pPedido.Id);
                if (pedido != null)
                {
                    pedido.IdUsuario = pPedido.IdUsuario;
                    pedido.Fecha = pPedido.Fecha;
                    pedido.IdProducto = pPedido.IdProducto;
                    pedido.Cantidad = pPedido.Cantidad;
                    pedido.PrecioUnitario = pPedido.PrecioUnitario;
                    pedido.Ubicacion = pPedido.Ubicacion;
                    pedido.Telefono = pPedido.Telefono;
                    pedido.CostoDelivery = pPedido.CostoDelivery;
                    bdContexto.Update(pedido);
                    result = await bdContexto.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Pedido pPedido)
        {
            int result = 0;
            using (var bdContexto = new BdContext())
            {
                var pedido = await bdContexto.Pedido.FirstOrDefaultAsync(s => s.Id == pPedido.Id);
                if (pedido != null)
                {
                    bdContexto.Pedido.Remove(pedido);
                    result = await bdContexto.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<Pedido> ObtenerPorIdAsync(Pedido pPedido)
        {
            Pedido pedido = null;
            using (var bdContexto = new BdContext())
            {
                pedido = await bdContexto.Pedido.FirstOrDefaultAsync(s => s.Id == pPedido.Id);
            }
            return pedido;
        }

        public static async Task<List<Pedido>> ObtenerTodosAsync()
        {
            List<Pedido> pedidos = null;
            using (var bdContexto = new BdContext())
            {
                pedidos = await bdContexto.Pedido.ToListAsync();
            }
            return pedidos;
        }

        internal static IQueryable<Pedido> QuerySelect(IQueryable<Pedido> pQuery, Pedido pPedido)
        {
            if (pPedido.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pPedido.Id);
            if (pPedido.IdUsuario > 0)
                pQuery = pQuery.Where(s => s.IdUsuario == pPedido.IdUsuario);
            if (pPedido.IdProducto > 0)
                pQuery = pQuery.Where(s => s.IdProducto == pPedido.IdProducto);
            if (pPedido.Fecha.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pPedido.Fecha.Year, pPedido.Fecha.Month, pPedido.Fecha.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.Fecha >= fechaInicial && s.Fecha <= fechaFinal);
            }
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pPedido.Top_Aux > 0)
                pQuery = pQuery.Take(pPedido.Top_Aux).AsQueryable();
            return pQuery;
        }

        public static async Task<List<Pedido>> BuscarAsync(Pedido pPedido)
        {
            List<Pedido> pedidos = null;
            using (var bdContexto = new BdContext())
            {
                var select = bdContexto.Pedido.AsQueryable();
                select = QuerySelect(select, pPedido);
                pedidos = await select.ToListAsync();
            }
            return pedidos;
        }
    }
}

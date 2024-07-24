using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysCafeteria.AccesoADatos;
using SysCafeteria.EntidadDeNegocio;
using SysCafeteria.EntidadesDeNegocio;
using SysCafeteria.LogicaDeNegocio;
using System.Reflection.Metadata.Ecma335;

namespace SysCafeteria.WebAPI.Controllers
{
    [Route("api/categoria")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        [HttpGet(Name = "GetCategorias")]
        public async Task<List<Categoria>> Get()
        {
            //Investigar response http (404, 200, 500, 501)
            var listacategoria = await CategoriaDAL.ObtenerTodosAsync();
            if (listacategoria.Count >= 1)
            {
                return listacategoria;
            }
            else
            {
                return new List<Categoria>();
            }
        }
        [HttpPost(Name = "PostCategoria")]
        public async Task<int> Post(Categoria pCategoria)
        {
            if (pCategoria.Id >= 0)
            {
                int resultado = await CategoriaDAL.CrearAsync(pCategoria);
                return resultado;
            }
            return 0;
        }
        [HttpDelete(Name = "DeleteCategoria")]
        public async Task<int> Delete(int id, Categoria pCategoria)
        {
            if (id >= 1)
            {

                int resultado = await CategoriaDAL.EliminarAsync(pCategoria);
                return 1;
            }
            return 0;
        }

        [HttpPut(Name = "PutCategoria")]
        public async Task<int> Put(int id, [FromBody] Categoria pCategoria)
        {

            if (pCategoria.Id >= 0)
            {
                int resultado = await CategoriaDAL.ModificarAsync(pCategoria);
                return resultado;
            }
            else
            {
                return 0;
            }


        }
    }

}

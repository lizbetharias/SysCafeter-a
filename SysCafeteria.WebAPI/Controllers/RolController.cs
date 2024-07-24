using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysCafeteria.AccesoADatos;
using SysCafeteria.EntidadesDeNegocio;

namespace SysCafeteria.WebAPI.Controllers
{
    [Route("api/rol")]
    [ApiController]
    public class RolController : ControllerBase
    {
        [HttpGet(Name = "GetRoles")]
        public async Task<List<Rol>> Get()
        {
            var listaroles = await RolDAL.ObtenerTodosAsync();
            if (listaroles.Count >= 1)
            {
                return listaroles;
            }
            else
            {
                return new List<Rol>();
            }

        }


        [HttpPost(Name = "PostRol")]
        public async Task<int> Post(Rol pRol)
        {
            if (pRol.Id >= 0)
            {
                int resultado = await RolDAL.CrearAsync(pRol);
                return resultado;

            }
            else
            {
                return 0;
            }
        }

        [HttpPut(Name = "PutRol")]
        public async Task<int> Put(int id, [FromBody] Rol pRol)
        {

            if (pRol.Id >= 0)
            {
                int resultado = await RolDAL.ModificarAsync(pRol);
                return resultado;
            }
            else
            {
                return 0;
            }

        }

        [HttpDelete(Name = "DeleteRol")]
        public async Task<int> Delete(int id, Rol pRol)
        {
            if (id >= 1)
            {

                int resultado = await RolDAL.EliminarAsync(pRol);
                return 1;
            }
            return 0;
        }

    }
}

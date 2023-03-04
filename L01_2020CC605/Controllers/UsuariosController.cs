using Microsoft.AspNetCore.Mvc;
using L01_2020CC605.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace L01_2020CC605.Controllers
{
    [Route("api/controller")]
    [ApiController]



    public class UsuariosController : ControllerBase
    {
        private readonly calificacionesContext _calificacionesContext;
        public UsuariosController(calificacionesContext calificacionesContext)
        {
            _calificacionesContext = calificacionesContext;
        }

        /// <summary>
        /// Mostrando lista de todos los usuarios
        /// </summary>


        [HttpGet]
        [Route("getall")]

        public IActionResult ObtenerUsuarios()
        {
            List<usuarios> listadousuarios = (from e in _calificacionesContext.usuarios
                                                          select e).ToList();
            if (listadousuarios.Count == 0) { return NotFound(); }

            return Ok(listadousuarios);
        }

        /// <returns> Retorna la lista de todos lss ususarios</returns>
        /// 

        // Agrega un nuevo usuario //

        [HttpPost]
        [Route("add")]
        public IActionResult ingresarUsuario([FromBody] usuarios usuarioNuevo)
        {
            try
            {
                _calificacionesContext.usuarios.Add(usuarioNuevo);
                _calificacionesContext.SaveChanges();

                return Ok(usuarioNuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Termina el agregar un nuevo usuario //



        // Actualizar datos de un usuario //

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult actualizarusuario(int id, [FromBody] usuarios usuarioModificar)
        {
            usuarios? usuarioExiste = (from e in _calificacionesContext.usuarios
                                                  where e.usuarioId == id
                                                  select e).FirstOrDefault();

            if (usuarioExiste == null) { return NotFound(); }

            usuarioExiste.rolId = usuarioModificar.rolId;
            usuarioExiste.nombreUsuario = usuarioModificar.nombreUsuario;
            usuarioExiste.clave = usuarioModificar.clave;
            usuarioExiste.nombre = usuarioModificar.nombre;
            usuarioExiste.apellido = usuarioModificar.apellido;



            _calificacionesContext.Entry(usuarioExiste).State = EntityState.Modified;
            _calificacionesContext.SaveChanges();

            return Ok(usuarioExiste);

        }

        // Termina actualizar datos de un usuario //


        // comienza eliminar un usuario //

        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult eliminarUsuario(int id)
        {
            usuarios? usuarioExiste = (from e in _calificacionesContext.usuarios
                                                  where e.usuarioId == id
                                                  select e).FirstOrDefault();

            if (usuarioExiste == null) return NotFound();

            _calificacionesContext.usuarios.Attach(usuarioExiste);
            _calificacionesContext.usuarios.Remove(usuarioExiste);
            _calificacionesContext.SaveChanges();

            return Ok(usuarioExiste);

        }

        // Termina eliminar una usuario //

        // comienza buscar por nombre y apellido


        [HttpGet]
        [Route("find/{nombre}/{apellido}")]

        public IActionResult buscarNombreApellido(string nombre, string apellido)
        {
            List<usuarios> usuariosList = (from e in _calificacionesContext.usuarios
                                         where e.nombre.Contains(nombre)
                                         || e.apellido.Contains(apellido)
                                         select e).ToList();

            if (usuariosList.Any()) { return Ok(usuariosList); }

            return NotFound();

        }

        // termina buscar por nombre y apellido

        // comienza filtro por rolId


        [HttpGet]
        [Route("findbyRol/{rolId}")]

        public IActionResult buscarRolId(int rolId)
        {
            List<usuarios> usuariosList = (from e in _calificacionesContext.usuarios
                                           where e.rolId == rolId
                                           select e).ToList();

            if (usuariosList.Any()) { return Ok(usuariosList); }

            return NotFound();

        }

        // termina filtro por rolId

    }
}

using Microsoft.AspNetCore.Mvc;
using L01_2020CC605.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace L01_2020CC605.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly calificacionesContext _calificacionesContext;
        public ComentariosController(calificacionesContext calificacionesContext)
        {
            _calificacionesContext = calificacionesContext;
        }

        /// <summary>
        /// Mostrando lista de todas las calificaciones
        /// </summary>


        [HttpGet]
        [Route("getall")]

        public IActionResult ObtenerComentarios()
        {
            List<comentarios> listadocomentarios = (from e in _calificacionesContext.comentarios
                                              select e).ToList();
            if (listadocomentarios.Count == 0) { return NotFound(); }

            return Ok(listadocomentarios);
        }

        /// <returns> Retorna la lista de todas las calificaiones</returns>
        /// 

        // Agrega un nuevo comentario //
        [HttpPost]
        [Route("add")]
        public IActionResult ingresarComentario([FromBody] comentarios comentarioNuevo)
        {
            try
            {
                _calificacionesContext.comentarios.Add(comentarioNuevo);
                _calificacionesContext.SaveChanges();

                return Ok(comentarioNuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Termina el agregar un comentario //



        // Actualizar datos de un comentario //

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult actualizarComentario(int id, [FromBody] comentarios comentarioModificar)
        {
            comentarios? comentarioExiste = (from e in _calificacionesContext.comentarios
                                       where e.cometarioId == id
                                       select e).FirstOrDefault();

            if (comentarioExiste == null) { return NotFound(); }

            comentarioExiste.publicacionId = comentarioModificar.publicacionId;
            comentarioExiste.comentario = comentarioModificar.comentario;
            comentarioExiste.usuarioId = comentarioModificar.usuarioId;



            _calificacionesContext.Entry(comentarioExiste).State = EntityState.Modified;
            _calificacionesContext.SaveChanges();

            return Ok(comentarioExiste);

        }

        // Termina actualizar datos de un comentario //


        // Eliminar un comentario //

        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult eliminarComentario(int id)
        {
            comentarios? comentarioExiste = (from e in _calificacionesContext.comentarios
                                       where e.cometarioId == id
                                       select e).FirstOrDefault();

            if (comentarioExiste == null) return NotFound();

            _calificacionesContext.comentarios.Attach(comentarioExiste);
            _calificacionesContext.comentarios.Remove(comentarioExiste);
            _calificacionesContext.SaveChanges();

            return Ok(comentarioExiste);

        }

        // termina eliminar un comentario //


        // busqueda por usuario especifico

        [HttpGet]
        [Route("find")]

        public IActionResult buscarporusuario(int filtro)
        {
            List<comentarios> comentariosList = (from e in _calificacionesContext.comentarios
                                                       where e.usuarioId == filtro
                                                       select e).ToList();

            if (comentariosList.Any()) { return Ok(comentariosList); }

            return NotFound();
        }

        // termina busqueda por usuario especifico



    }
}

using Microsoft.AspNetCore.Mvc;
using L01_2020CC605.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace L01_2020CC605.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {
        private readonly calificacionesContext _calificacionesContext;
        public CalificacionesController(calificacionesContext calificacionesContext)
        {
            _calificacionesContext = calificacionesContext;
        }

        /// <summary>
        /// Mostrando lista de todas las calificaciones
        /// </summary>
        

        [HttpGet]
        [Route("getall")]

        public IActionResult ObtenerCalificaciones() {
            List<calificaciones> listadoCalificaciones = (from e in _calificacionesContext.calificaciones
                                                          select e).ToList();
            if (listadoCalificaciones.Count == 0) { return NotFound(); }

            return Ok(listadoCalificaciones);
        }

        /// <returns> Retorna la lista de todas las calificaiones</returns>


        // Agrega un nuevo registro //
        [HttpPost]
        [Route("add")]
        public IActionResult ingresarCalificacion([FromBody] calificaciones calificacionNueva)
        {
            try
            {
                _calificacionesContext.calificaciones.Add(calificacionNueva);
                _calificacionesContext.SaveChanges();

                return Ok(calificacionNueva);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Termina el agregar una nueva calificacion //



        // Actualizar datos de una calificacion //

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult actualizarCalificacion(int id, [FromBody] calificaciones calificacionModificar)
        {
            calificaciones? calificacionExiste = (from e in _calificacionesContext.calificaciones
                                                  where e.calificacionId == id
                                                  select e).FirstOrDefault();

            if (calificacionExiste == null) { return NotFound(); }

            calificacionExiste.publicacionId = calificacionModificar.publicacionId;
            calificacionExiste.usuarioId = calificacionModificar.usuarioId;
            calificacionExiste.calificacion = calificacionModificar.calificacion;

            _calificacionesContext.Entry(calificacionExiste).State = EntityState.Modified;
            _calificacionesContext.SaveChanges();

            return Ok(calificacionExiste);

        }

        // Termina actualizar datos de una calificacion //


        // Eliminar una calificacion //

        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult eliminarCalificacion(int id)
        {
            calificaciones? calificacionExiste = (from e in _calificacionesContext.calificaciones
                                                  where e.calificacionId == id
                                                  select e).FirstOrDefault();

            if (calificacionExiste == null)  return NotFound();

            _calificacionesContext.calificaciones.Attach(calificacionExiste);
            _calificacionesContext.calificaciones.Remove(calificacionExiste);
            _calificacionesContext.SaveChanges();

            return Ok(calificacionExiste);

        }

        // Termina eliminar una calificacion //


        // Buscar calificación por publicacion //

        [HttpGet]
        [Route("find")]

        public IActionResult buscarporpublicacion(int publicacionId)
        {
            List<calificaciones> calificacionesList = (from e in _calificacionesContext.calificaciones
                                                       where e.publicacionId == publicacionId
                                                       select e).ToList();

            if (calificacionesList.Any()) { return Ok(calificacionesList); }

            return NotFound();
        }

        // Buscar calificación por publicacion //
    }
}

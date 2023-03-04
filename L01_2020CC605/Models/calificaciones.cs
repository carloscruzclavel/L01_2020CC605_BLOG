using System.ComponentModel.DataAnnotations;

namespace L01_2020CC605.Models
{
    public class calificaciones
    {
        [Key]

        public int calificacionId { get; set; }
        public int publicacionId { get; set; }
        public int usuarioId { get; set; }
        public int calificacion { get; set; }

    }

    public class usuarios
    {
        [Key]

        public int usuarioId { get; set; }
        public int rolId { get; set;}
        public string nombreUsuario { get; set;}
        public string clave { get; set; }
        public string nombre { get; set;}
        public string apellido { get; set;}

    }

    public class comentarios
    {
        [Key]

        public int cometarioId { get; set; }
        public int publicacionId { get; set; }
        public string comentario { get; set; }
        public int usuarioId { get; set; }
    }
}

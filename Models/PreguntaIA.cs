using System;

namespace AsistenteOpenAI.Models
{
    public class PreguntaIA
    {
        public int Id { get; set; } // Identificador para la Base de Datos
        public string Estudiante { get; set; }
        public string Asignatura { get; set; }
        public string Texto { get; set; }

        // Propiedad de navegación para la relación 1 a 1 en EF Core
        public RespuestaIA? Respuesta { get; set; }

        public PreguntaIA() { } // Constructor vacío para EF Core

        public PreguntaIA(string estudiante, string asignatura, string texto)
        {
            if (string.IsNullOrWhiteSpace(estudiante))
            {
                throw new ArgumentException("El nombre del estudiante no puede estar vacío.", nameof(estudiante));
            }
            if (string.IsNullOrWhiteSpace(asignatura))
            {
                throw new ArgumentException("El nombre de la asignatura no puede estar vacío.", nameof(asignatura));
            }
            if (string.IsNullOrWhiteSpace(texto))
            {
                throw new ArgumentException("El texto de la pregunta no puede estar vacío.", nameof(texto));
            }

            Estudiante = estudiante;
            Asignatura = asignatura;
            Texto = texto;
        }
    }
}
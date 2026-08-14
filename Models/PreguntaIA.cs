using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenteOpenAI.Models
{
    public class PreguntaIA
    {

        public String Estudiante { get; set; }
        public String Asignatura { get; set; }
        public String Texto { get; set; }
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
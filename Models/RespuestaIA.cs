using System;

namespace AsistenteOpenAI.Models
{
    public class RespuestaIA
    {
        public int Id { get; set; } // Identificador para la Base de Datos
        public string Texto { get; set; }
        public string ModeloUtilizado { get; set; }
        public DateTime Fecha { get; set; }

        // Llave foránea hacia PreguntaIA
        public int PreguntaIAId { get; set; }
        public PreguntaIA PreguntaIA { get; set; }

        public RespuestaIA() { } // Constructor vacío para EF Core

        public RespuestaIA(string texto, string modeloUtilizado)
        {
            Texto = texto;
            ModeloUtilizado = modeloUtilizado;
            Fecha = DateTime.Now;
        }
    }
}

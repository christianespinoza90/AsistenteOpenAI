using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenteOpenAI.Models
{
    public class RespuestaIA
    {

        public string Texto { get; set; }
        public string ModeloUtilizado { get; set; }
        public DateTime Fecha { get; set; }

        public RespuestaIA(string texto, string modeloUtilizado)
        {
            Texto = texto;
            ModeloUtilizado = modeloUtilizado;
            Fecha = DateTime.Now;
        }



    }
}
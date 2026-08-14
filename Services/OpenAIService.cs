using AsistenteOpenAI.Interfaces;
using AsistenteOpenAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenteOpenAI.Services
{
    public class OpenAIService : IAsistenteIA
    {
        public Task<RespuestaIA> PreguntarAsync(PreguntaIA pregunta)
        {
            throw new NotImplementedException();
        }
    }
}
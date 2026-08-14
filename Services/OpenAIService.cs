using AsistenteOpenAI.Interfaces;
using AsistenteOpenAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI.Responses;

namespace AsistenteOpenAI.Services
{
    public class OpenAIService : IAsistenteIA
    {

#pragma warning disable OPENAI001
        private readonly ResponsesClient cliente;
        private readonly string modelo;

        public OpenAIService(string modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo))
            {
                throw new ArgumentException("El nombre del modelo no puede estar vacío.", nameof(modelo));
            }

            this.cliente = new ResponsesClient("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");
            this.modelo = modelo;
        }

        public async Task<RespuestaIA> PreguntarAsync(PreguntaIA pregunta)
        {
            if (pregunta == null)
            {
                throw new ArgumentNullException(nameof(pregunta), "La pregunta no puede ser nula.");
            }

            string instrucciones = $"Eres un asistente de IA que ayuda a los estudiantes con sus preguntas sobre {pregunta.Asignatura}." +
                $"El estudiante se llama {pregunta.Estudiante} Responde de manera clara y concisa." +
                $"Pregunta del estudiante: {pregunta.Texto}";

            ResponseResult resultado = await cliente.CreateResponseAsync(modelo, instrucciones);

            string textoRespuesta = resultado.GetOutputText();
            RespuestaIA respuestaIA = new RespuestaIA(textoRespuesta, modelo);
            return respuestaIA;
        }
    }
}
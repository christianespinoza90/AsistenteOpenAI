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
using AsistenteOpenAI.Interfaces;
using AsistenteOpenAI.Models;

namespace AsistenteOpenAI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Bienvenido al Asistente de IA para estudiantes.");
            Console.Write("Ingrese su nombre: ");
            string estudiante = Console.ReadLine();
            Console.Write("Ingrese la asignatura: ");
            string asignatura = Console.ReadLine();
            Console.Write("Ingrese su pregunta: ");
            string textoPregunta = Console.ReadLine();
            PreguntaIA pregunta = new PreguntaIA(estudiante, asignatura, textoPregunta);
            IAsistenteIA asistenteIA = new Services.OpenAIService("gpt-4o-mini");
            try
            {
                RespuestaIA respuesta = await asistenteIA.PreguntarAsync(pregunta);
                Console.WriteLine($"\nRespuesta del asistente:\n{respuesta.Texto}");
                Console.WriteLine($"\nModelo utilizado: {respuesta.ModeloUtilizado}");
                Console.WriteLine($"Fecha de respuesta: {respuesta.Fecha}");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al procesar la pregunta: {ex.Message}");
            }
        }
    }
}
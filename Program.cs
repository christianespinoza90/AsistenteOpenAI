using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AsistenteOpenAI.Services;
using AsistenteOpenAI.Datos;
using AsistenteOpenAI.Interfaces;
using AsistenteOpenAI.Models;

bool continuar = true;

// Este ciclo mantendrá el programa vivo hasta que decidas salir
while (continuar)
{
    await RealizarPreguntaAsync();

    Console.WriteLine("\n================================================");
    Console.Write("¿Deseas realizar otra pregunta? (S para Sí / N para Salir): ");
    string respuesta = Console.ReadLine();

    if (respuesta?.Trim().ToUpper() != "S")
    {
        continuar = false; // Rompe el ciclo y cierra el programa
    }
}

Console.WriteLine("\n¡Gracias por usar el Asistente IA! Cerrando sistema...");
await Task.Delay(1500); // Espera un segundito antes de cerrar para que se vea bonito


// AQUÍ EMPIEZA TU MÉTODO ORIGINAL
static async Task RealizarPreguntaAsync()
{
    Console.Clear();
    Console.WriteLine("********** Crear Pregunta a la IA **********");
    Console.Write("Ingrese su nombre: ");
    string estudiante = Console.ReadLine();
    Console.Write("Ingrese la asignatura: ");
    string asignatura = Console.ReadLine();
    Console.Write("Ingrese su pregunta: ");
    string textoPregunta = Console.ReadLine();

    try
    {
        using (var context = new AsistenteDbContext())
        {
            // 1. BUSCAR SI LA PREGUNTA YA EXISTE EN LA BASE DE DATOS
            var preguntaExistente = context.Preguntas
                .Include(p => p.Respuesta)
                .FirstOrDefault(p =>
                    p.Texto.Trim().ToLower() == textoPregunta.Trim().ToLower() &&
                    p.Asignatura.Trim().ToLower() == asignatura.Trim().ToLower());

            if (preguntaExistente != null && preguntaExistente.Respuesta != null)
            {
                // 2. LA PREGUNTA YA SE HIZO ANTES -> MOSTRAR RESPUESTA GUARDADA
                Console.WriteLine("\n[✅ Respuesta encontrada en la Base de Datos Local]");
                Console.WriteLine("--- Respuesta Recuperada ---");
                Console.WriteLine(preguntaExistente.Respuesta.Texto);
                Console.WriteLine($"\nModelo: {preguntaExistente.Respuesta.ModeloUtilizado} (Desde Caché SQL)");
                Console.WriteLine($"Fecha original: {preguntaExistente.Respuesta.Fecha:dd/MM/yyyy HH:mm}");
            }
            else
            {
                // 3. LA PREGUNTA ES NUEVA -> CONSULTAR A OPENAI
                PreguntaIA nuevaPregunta = new PreguntaIA(estudiante, asignatura, textoPregunta);
                IAsistenteIA asistenteIA = new OpenAIService("gpt-4o-mini");

                Console.WriteLine("\n[🌐 Pregunta nueva. Consultando a OpenAI...]");
                RespuestaIA nuevaRespuesta = await asistenteIA.PreguntarAsync(nuevaPregunta);

                // 4. GUARDAR LA NUEVA PREGUNTA Y RESPUESTA EN SQL SERVER
                context.Preguntas.Add(nuevaPregunta);
                context.SaveChanges();

                nuevaRespuesta.PreguntaIAId = nuevaPregunta.Id;
                context.Respuestas.Add(nuevaRespuesta);
                context.SaveChanges();

                Console.WriteLine("\n--- Respuesta de OpenAI ---");
                Console.WriteLine(nuevaRespuesta.Texto);
                Console.WriteLine($"\nModelo: {nuevaRespuesta.ModeloUtilizado}");
                Console.WriteLine($"Fecha: {nuevaRespuesta.Fecha:dd/MM/yyyy HH:mm}");
                Console.WriteLine("\n¡La nueva pregunta y respuesta han sido guardadas en SQL Server!");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nOcurrió un error: {ex.Message}");
    }
}
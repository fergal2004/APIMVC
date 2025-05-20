using APIMVC.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.Json; // Importa System.Text.Json para la deserialización

namespace APIMVC.Controllers
{
    public class ChatbotController : Controller
    {
        private readonly IChatbotService _chatbotService;

        public ChatbotController(IChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        public IActionResult Index()
        {
            ViewBag.NombreChatbot = "FJM";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Preguntar(string pregunta)
        {
            ViewBag.NombreChatbot = "FJM";

            if (string.IsNullOrEmpty(pregunta))
            {
                ViewBag.ErrorMessage = "Por favor, ingresa tu pregunta.";
                return View("Index");
            }

            var jsonResponse = await _chatbotService.ObtenerRespuestaCahtbot(pregunta);
            bool guardado = _chatbotService.GuardarRespuestaBDD(jsonResponse, "gemini", "sistema", pregunta);

            string respuestaTexto = "";
            try
            {
                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                if (doc.RootElement.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
                {
                    if (candidates[0].TryGetProperty("content", out var content) && content.TryGetProperty("parts", out var parts) && parts.GetArrayLength() > 0)
                    {
                        if (parts[0].TryGetProperty("text", out var text))
                        {
                            respuestaTexto = text.GetString();
                        }
                    }
                }
            }
            catch (JsonException ex)
            {
                // Manejar el error de deserialización (opcional)
                ViewBag.ErrorMessage = "Error al procesar la respuesta de la IA.";
                // Puedes registrar el error aquí
            }

            ViewBag.Pregunta = pregunta;
            ViewBag.Respuesta = respuestaTexto; // Ahora pasamos el texto extraído
            ViewBag.GuardadoExitoso = guardado;
            
            Console.WriteLine($"Respuesta en el controlador: {ViewBag.Respuesta}");
            
            return View("Index");
        }
    }
}

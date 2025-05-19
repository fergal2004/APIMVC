using System.Text;
using APIMVC.Interfaces;
using APIMVC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using APIMVC.Data;
using Newtonsoft.Json; 

namespace APIMVC.Repositories;

public class GeminiRepository : IChatbotService
{
    private HttpClient _httpclient;
    private readonly string geminiApiKey = "AIzaSyBU6XP4z_5U3jsZuQRqmSrhZE-0rGKzrQY";
    private readonly string _connectionString;
    private readonly ILogger<GeminiRepository> _logger;
    private readonly ApplicationDbContext _dbContext; 

    public GeminiRepository(IConfiguration configuration, ILogger<GeminiRepository> logger, ApplicationDbContext dbContext) // Modifica el constructor
    {
        _httpclient = new HttpClient();
        _connectionString = configuration.GetConnectionString("APIMVC");
        _logger = logger;
        _dbContext = dbContext; // Inicializa el DbContext
    }

    public async Task<string> ObtenerRespuestaCahtbot(string prompt)
    {
        string url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=" + geminiApiKey;

        GeminiRequest request = new GeminiRequest()
        {
            contents = new List<GeminiContent>
            {
                new GeminiContent
                {
                    parts = new List<GeminiPart>
                    {
                        new GeminiPart
                        {
                            text = prompt
                        }
                    }
                }
            }
        };

        string json_data = JsonConvert.SerializeObject(request);
        var content = new StringContent(json_data, Encoding.UTF8, "application/json");
        var response = await _httpclient.PostAsync(url, content);

        return await response.Content.ReadAsStringAsync();
    }

    public bool GuardarRespuestaBDD(string respuesta, string proveedor, string guardadoPor, string prompt)
    {
        try
        {
            var respuestaChatbot = new RespuestaChatbot 
            {
                Respuesta = respuesta,
                Fecha = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                Proveedor = proveedor,
                GuardadoPor = guardadoPor,
                Pregunta = prompt
            };

            _dbContext.RespuestasChatbot.Add(respuestaChatbot); 
            _dbContext.SaveChanges(); 

            _logger.LogInformation("Respuesta guardada en la base de datos.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al guardar en la base de datos: {Message}", ex.Message);
            return false;
        }
    }
}



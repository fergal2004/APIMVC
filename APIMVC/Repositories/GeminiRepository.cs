using System.Text;
using APIMVC.Interfaces;
using APIMVC.Models;
using Newtonsoft.Json;

namespace APIMVC.Repositories;

public class GeminiRepository : IChatbotService
{
    
    private HttpClient _httpclient;
    private readonly string geminiApiKey = "AIzaSyBU6XP4z_5U3jsZuQRqmSrhZE-0rGKzrQY";
    
    public GeminiRepository()
    {
        _httpclient = new HttpClient();
    }
    
    
    
    public async Task<string> ObtenerRespuestaCahtbot(string prompt)
    {
        String url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=" + geminiApiKey;
        
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
    
    public bool GruardarRespuestaBDD(string prompt, string respuesta)
    {
        throw new NotImplementedException();
    }
    
}
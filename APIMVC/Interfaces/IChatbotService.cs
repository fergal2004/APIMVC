namespace APIMVC.Interfaces;

public interface IChatbotService
{
    public Task<string> ObtenerRespuestaCahtbot(string prompt);
    
    public bool GruardarRespuestaBDD(string prompt, string respuesta);
}
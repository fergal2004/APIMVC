namespace APIMVC.Interfaces;

public interface IChatbotService
{
    public Task<string> ObtenerRespuestaCahtbot(string prompt);

    public bool GuardarRespuestaBDD(string respuesta, string proveedor, string guardadoPor, string prompt);
}
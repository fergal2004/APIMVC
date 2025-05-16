using APIMVC.Interfaces;

namespace APIMVC.Repositories;

public class OpenIARepository : IChatbotService
{
    public bool GruardarRespuestaBDD(string prompt, string respuesta)
    {
        throw new NotImplementedException();
    }
    
    public Task<string> ObtenerRespuestaCahtbot(string prompt)
    {
        throw new NotImplementedException();
    }
}
using System.Diagnostics;
using APIMVC.Interfaces;
using Microsoft.AspNetCore.Mvc;
using APIMVC.Models;
using APIMVC.Repositories;

namespace APIMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IChatbotService _chatbotService;

    public HomeController(IChatbotService chatbotService)
    {
       // _logger = logger;
        //_chatbotService = new GeminiRepository();
        _chatbotService = chatbotService;
    }

    public async Task<IActionResult> Index()
    {
        var respuesta = await _chatbotService.ObtenerRespuestaCahtbot("Resume de 100 palabras de Titanic");
        ViewBag.respuesta = respuesta;
        return View();
    }
    
}
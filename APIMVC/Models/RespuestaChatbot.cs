using System;
using System.ComponentModel.DataAnnotations;

namespace APIMVC.Models;

public class RespuestaChatbot
{
    [Key]
    public int Id { get; set; }
    public string Respuesta { get; set; }
    public DateTime Fecha { get; set; }
    public string Proveedor { get; set; }
    public string GuardadoPor { get; set; }
    public string Pregunta { get; set; }
}
using APIMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMVC.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<RespuestaChatbot> RespuestasChatbot { get; set; }
}
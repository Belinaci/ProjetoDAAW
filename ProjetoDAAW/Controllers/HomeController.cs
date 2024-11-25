using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoDAAW.Data;
using ProjetoDAAW.Models;
using System.Diagnostics;

namespace ProjetoDAAW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Pegando os 10 filmes mais recentes
            var filmes = await _context.Filme
                                        .Include(f => f.Genero)
                                        .Include(f => f.Artista)
                                        .Include(f => f.Personagem)
                                        .OrderByDescending(f => f.Id) // Supondo que você tem um campo DataAdicionado
                                        .Take(10) // Pegando os 10 mais recentes
                                        .ToListAsync();

            return View(filmes); // Passando para a View
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

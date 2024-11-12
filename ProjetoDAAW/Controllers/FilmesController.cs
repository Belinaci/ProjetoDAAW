using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoDAAW.Data;
using ProjetoDAAW.Models;

namespace ProjetoDAAW.Controllers
{
    public class FilmesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilmesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Filmes
        public async Task<IActionResult> Index()
        {
            // Inclui os Gêneros e Filmes a serem mostrados no Index
            var filmes = await _context.Filme
                .Include(f => f.Genero)    
                .Include(f => f.Artista)   
                .ToListAsync();

            return View(await _context.Filme.ToListAsync());
        }

        // GET: Filmes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        /* ORIGINAL BACKUP
        // GET: Filmes/Create
        public IActionResult Create()
        {

            // Traz a lista de Gêneros e Atores
            ViewBag.Genero = new MultiSelectList(_context.Genero, "Id", "Nome");
            ViewBag.Artista = new MultiSelectList(_context.Artista, "Id", "Nome");
            return View();
        }

        // POST: Filmes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descricao,GeneroId,DtLancamento,ArtistaId,Diretor,FtCapaFilme")] Filme filme)
        {
            if (ModelState.IsValid)
            {
                // Adiciona os gêneros selecionados à lista de gêneros do filme
                filme.Genero = _context.Genero.Where(g => generoSelecionados.Contains(g.Id)).ToList();

                // Adiciona os artistas selecionados à lista de artistas do filme
                filme.Artista = _context.Artista.Where(a => artistaSelecionados.Contains(a.Id)).ToList();

                _context.Add(filme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filme);
        }
        */

        // GET: Filmes/Create
        public IActionResult Create()
        {
            // Traz a lista de Gêneros e Artistas
            ViewBag.Genero = new MultiSelectList(_context.Genero, "Id", "Nome");
            ViewBag.Artista = new MultiSelectList(_context.Artista, "Id", "Nome");
            return View();
        }

        // POST: Filmes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descricao,DtLancamento,Diretor,FtCapaFilme")] Filme filme, int[] generoSelecionados, int[] artistaSelecionados)
        {
            // remove validação de Genero e Artista pq a gente esqueceu de adicionar o "?" na list<>
            ModelState.Remove("Genero");
            ModelState.Remove("Artista");

            if (ModelState.IsValid)
            {
                // Adiciona os gêneros selecionados à lista de gêneros do filme
                if (generoSelecionados != null && generoSelecionados.Length > 0)
                {
                    filme.Genero = _context.Genero
                        .Where(g => generoSelecionados.Contains(g.Id))
                        .ToList();
                }

                // Adiciona os artistas selecionados à lista de artistas do filme
                if (artistaSelecionados != null && artistaSelecionados.Length > 0)
                {
                    filme.Artista = _context.Artista
                        .Where(a => artistaSelecionados.Contains(a.Id))
                        .ToList();
                }

                // Salva o filme no banco de dados
                _context.Add(filme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Se o modelo não for válido, repopula as listas de gêneros e artistas
            ViewBag.Genero = new MultiSelectList(_context.Genero, "Id", "Nome");
            ViewBag.Artista = new MultiSelectList(_context.Artista, "Id", "Nome");
            return View(filme);
        }



        // GET: Filmes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme.FindAsync(id);
            if (filme == null)
            {
                return NotFound();
            }
            return View(filme);
        }

        // POST: Filmes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,GeneroId,DtLancamento,ArtistaId,Diretor,FtCapaFilme")] Filme filme)
        {
            if (id != filme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmeExists(filme.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(filme);
        }

        // GET: Filmes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        // POST: Filmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filme = await _context.Filme.FindAsync(id);
            if (filme != null)
            {
                _context.Filme.Remove(filme);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmeExists(int id)
        {
            return _context.Filme.Any(e => e.Id == id);
        }
    }
}

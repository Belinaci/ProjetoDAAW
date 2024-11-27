using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<IActionResult> Index(string Filtro)
        {

            List<Filme> filmes;

            if (Filtro != null) 
            {
                // Inclui os Gêneros e Filmes a serem mostrados no Index, lazy loading é um bastardo
                    filmes = await _context.Filme
                    .Where(f => f.Titulo.Contains(Filtro) ||
                        f.Descricao.Contains(Filtro) ||
                        f.Genero.Any(g => g.Nome.Contains(Filtro)) ||
                        f.Artista.Any(a => a.Nome.Contains(Filtro)))
                    .Include(f => f.Genero)
                    .Include(f => f.Artista)
                    .ToListAsync();
            }
            else
            {
                // Inclui os Gêneros e Filmes a serem mostrados no Index, lazy loading é um bastardo
                    filmes = await _context.Filme
                    .Include(f => f.Genero) // Inclui os gêneros relacionados
                    .Include(f => f.Artista) // Inclui os Artistas relacionados
                    .ToListAsync();
            }

            return View(filmes);
        }

        // GET: Filmes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // lazy loading maldito
            var filme = await _context.Filme
                .Include(f => f.Genero)  
                .Include(f => f.Artista)
                .Include(f=> f.Personagem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        // GET: Filmes/Create
        [Authorize]

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
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // O FindAsync precisa ser substituído, ele não é feito para listas, portanto não carrega corretamente
            var filme = await _context.Filme
                .Include(f => f.Genero)  
                .Include(f => f.Artista) 
                .FirstOrDefaultAsync(f => f.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            // carrega e seleciona os gêneros e atores de cada filme
            ViewBag.Genero = new MultiSelectList(_context.Genero, "Id", "Nome", filme.Genero.Select(g => g.Id));
            ViewBag.Artista = new MultiSelectList(_context.Artista, "Id", "Nome", filme.Artista.Select(a => a.Id));

            return View(filme);
        }

        // POST: Filmes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,GeneroId,DtLancamento,ArtistaId,Diretor,FtCapaFilme")] Filme filme, int[] generoSelecionados, int[] artistaSelecionados)
        {
            // remove validação de Genero e Artista pq a gente esqueceu de adicionar o "?" na list<>
            ModelState.Remove("Genero");
            ModelState.Remove("Artista");

            if (id != filme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Carrega o filme existente do banco, isso é necessário pois o filme existe apenas na memória e não está sendo rastreado pelo EF
                    var filmeExistente = await _context.Filme
                        .Include(f => f.Genero)
                        .Include(f => f.Artista)
                        .FirstOrDefaultAsync(f => f.Id == id);


                    // Atualiza os campos do filme
                    filmeExistente.Titulo = filme.Titulo;
                    filmeExistente.Descricao = filme.Descricao;
                    filmeExistente.DtLancamento = filme.DtLancamento;
                    filmeExistente.Diretor = filme.Diretor;
                    filmeExistente.FtCapaFilme = filme.FtCapaFilme;

                    // É necessário outro procedimento para Gênero e Artista por conta da relação muitos para muitos.
                    filmeExistente.Genero = _context.Genero
                        .Where(g => generoSelecionados.Contains(g.Id))
                        .ToList();

                    
                    filmeExistente.Artista = _context.Artista
                        .Where(a => artistaSelecionados.Contains(a.Id))
                        .ToList();

                    // Salva no banco de dados
                    _context.Update(filmeExistente);
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

            // Repopula as listas de gêneros e artistas no caso de falha de validação
            ViewBag.Genero = new MultiSelectList(_context.Genero, "Id", "Nome", generoSelecionados);
            ViewBag.Artista = new MultiSelectList(_context.Artista, "Id", "Nome", artistaSelecionados);

            return View(filme);
        }

        // GET: Filmes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme
                .Include(f => f.Genero)  
                .Include(f => f.Artista)
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using ProjetoDAAW.Data;
using ProjetoDAAW.Models;

namespace ProjetoDAAW.Controllers
{
    public class PersonagensController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonagensController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Personagens
        [Authorize]
        public async Task<IActionResult> Index(string Filtro)
        {
            List<Personagem> personagens;

            if (Filtro != null)
            {
                // Inclui os Gêneros e Filmes a serem mostrados no Index, lazy loading é um bastardo
                personagens = await _context.Personagem
                .Where(p => p.Prsnag.Contains(Filtro))
                .Include(p => p.Filme)
                .Include(p => p.Artista)
                .ToListAsync();
            }
            else
            {
                // Inclui os Gêneros e Filmes a serem mostrados no Index, lazy loading é um bastardo
                personagens = await _context.Personagem
                .Include(p => p.Filme)
                .Include(p => p.Artista)
                .ToListAsync();
            }


            return View(personagens);
        }

        // GET: Personagens/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personagem = await _context.Personagem
                .Include(p => p.Filme)
                .Include(p => p.Artista)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personagem == null)
            {
                return NotFound();
            }

            return View(personagem);
        }

        // GET: Personagens/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.Filme = new MultiSelectList(_context.Filme, "Id", "Titulo");
            ViewBag.Artista = new MultiSelectList(_context.Artista, "Id", "Nome");
            return View();
        }

        // POST: Personagens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Prsnag")] Personagem personagem, int[] filmeSelecionados, int[] artistaSelecionados)
        {

            ModelState.Remove("Filme");
            ModelState.Remove("Artista");

            if (ModelState.IsValid)
            {
                if (filmeSelecionados != null && filmeSelecionados.Length > 0)
                {
                    personagem.Filme = _context.Filme
                        .Where(f => filmeSelecionados.Contains(f.Id))
                        .FirstOrDefault();
                }

                if (artistaSelecionados != null && artistaSelecionados.Length > 0)
                {
                    personagem.Artista = _context.Artista
                        .Where(a => artistaSelecionados.Contains(a.Id))
                        .FirstOrDefault();
                }

                _context.Add(personagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Filme = new MultiSelectList(_context.Filme, "Id", "Titulo");
            ViewBag.Artista = new MultiSelectList(_context.Artista, "Id", "Nome");
            return View(personagem);
        }

        // GET: Personagens/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personagem = await _context.Personagem.FindAsync(id);
            if (personagem == null)
            {
                return NotFound();
            }

                    // Popula os SelectLists para dropdowns
                    ViewData["FilmeId"] = new SelectList(_context.Filme, "Id", "Titulo", personagem.FilmeId);
                    ViewData["ArtistaId"] = new SelectList(_context.Artista, "Id", "Nome", personagem.ArtistaId);

            return View(personagem);
        }

        // POST: Personagens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Prsnag,FilmeId,ArtistaId")] Personagem personagem)
        {
            if (id != personagem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonagemExists(personagem.Id))
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
            return View(personagem);
        }

        // GET: Personagens/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personagem = await _context.Personagem
                .Include(a => a.Artista)
                .Include(f => f.Filme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personagem == null)
            {
                return NotFound();
            }

            return View(personagem);
        }

        // POST: Personagens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personagem = await _context.Personagem.FindAsync(id);
            if (personagem != null)
            {
                _context.Personagem.Remove(personagem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonagemExists(int id)
        {
            return _context.Personagem.Any(e => e.Id == id);
        }
    }
}

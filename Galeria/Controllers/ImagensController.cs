using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Galeria.Data;
using Galeria.Models;
using Microsoft.Extensions.Hosting;

namespace Galeria.Controllers
{
    public class ImagensController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHostEnvironment _env;

        public ImagensController(AppDbContext context, IHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
      
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Albuns, "AlbumId", "Destino");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImagemId,LinkImagem,AlbumId")] Imagem imagem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(imagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albuns, "AlbumId", "Destino", imagem.AlbumId);
            return View(imagem);
        }

    }
}

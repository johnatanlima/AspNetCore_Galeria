using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Galeria.Data;
using Galeria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.IO;
using System;

namespace Galeria.Controllers
{
    public class AlbunsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHostEnvironment _env;

        public AlbunsController(AppDbContext context, IHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Albuns
        public async Task<IActionResult> Index()
        {
            return View(await _context.Albuns.ToListAsync());
        }

        // GET: Albuns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albuns
                .FirstOrDefaultAsync(m => m.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albuns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Albuns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlbumId,Destino,FotoTopo,Inicio,Fim")] Album album, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var destinoFotos = Path.Combine(_env.ContentRootPath, "wwwwroot/Imagens");

                if(file != null)
                {
                    using (var fs = new FileStream(Path.Combine(destinoFotos, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fs);
                        album.FotoTopo = "~/Imagens/" + file.FileName;

                    }
                }

                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(album);
        }

        // GET: Albuns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albuns.FindAsync(id);
           
            if (album == null)
            {
                return NotFound();
            }

            TempData["FotoTopo"] = album.FotoTopo;
            
            return View(album);
        
            
        }

        // POST: Albuns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlbumId,Destino,FotoTopo,Inicio,Fim")] Album album, IFormFile file)
        {
            if (id != album.AlbumId)
            {
                return NotFound();
            }

            if (String.IsNullOrEmpty(album.FotoTopo))
                album.FotoTopo = TempData["FotoTopo"].ToString();

            if (ModelState.IsValid)
            {
                try
                {
                    var linkImagem = Path.Combine(_env.ContentRootPath, "wwwroot/Imagens");

                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.AlbumId))
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
            return View(album);
        }

        // POST: Albuns/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int AlbumId)
        {
            var album = await _context.Albuns.FindAsync(AlbumId);
            _context.Albuns.Remove(album);
            await _context.SaveChangesAsync();

            return Json("Album excluído com sucesso!");
        }

        private bool AlbumExists(int id)
        {
            return _context.Albuns.Any(e => e.AlbumId == id);
        }
    }
}

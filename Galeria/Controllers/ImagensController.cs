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
using Microsoft.AspNetCore.Http;
using System.IO;

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
      
        public IActionResult Create(int id)
        {

            ViewBag.Destinos = _context.Albuns.FirstOrDefault(x => x.AlbumId == id);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImagemId,LinkImagem,AlbumId")] Imagem imagem, IFormFile files)
        {
            if (ModelState.IsValid)
            {
                //Lógica => Monto um caminho na aplicação, concateno com o nome do arquivo que recebo, crio o arquivo com filestream no diretorio, copio o arquivo recebido no filestream, recupero caminho do arquivo inteiro, armazeno na propriedade do objeto.
                var caminhoFoto = Path.Combine(_env.ContentRootPath, "wwwroot/Imagens"); //Concatena o caminho do diretório Imagens, através do método combine, que recebe o caminho do diretório em que aplicação está sendo executada.

                using (var fs = new FileStream(Path.Combine(caminhoFoto, files.FileName), FileMode.Create)) //uso o caminho do diretório, e concateno com o nome da imagem que está vindo em minha requisição
                {
                    await files.CopyToAsync(fs); //copio a imagem da requisão para o arquivo filestream que criei no diretório logo acima.
                    imagem.LinkImagem = "~/Imagens/" + files.FileName;
                }

                _context.Add(imagem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Albuns", new {id = imagem.AlbumId});
            }
           
            ViewData["AlbumId"] = new SelectList(_context.Albuns, "AlbumId", "Destino", imagem.AlbumId);
            return View(imagem);
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Galeria.Models
{
    public class Imagem
    {
        public int ImagemId { get; set; }

        [Required(ErrorMessage ="Campo obrigatório")]
        public string LinkImagem { get; set; }

        public int AlbumId { get; set; }

        public Album Album { get; set; }

    }
}

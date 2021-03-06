﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Galeria.Models
{
    public class Album
    {
        public int AlbumId { get; set; }

        [Required(ErrorMessage ="Campo obrigatório.")]
        [StringLength(64, ErrorMessage ="Limite de caracteres excedido.")]
        public string Destino { get; set; }

        public string FotoTopo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime Inicio { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime Fim { get; set; }

        public ICollection<Imagem> Imagens { get; set; }

    }
}

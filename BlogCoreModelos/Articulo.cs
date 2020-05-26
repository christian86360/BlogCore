﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Modelos
{
   public class Articulo
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage ="El nombre es obligatorio")]
        [Display(Name ="Nombre del articulo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatorio")]
        public string Descripcion { get; set; }

        [Display(Name ="Fecha de creacion")]
        public string FechaCreacion { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name ="Imagen")]
        public string UrlImagen { get; set; }

        //relacion
        [Required]
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Modelos
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        [Column(TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Ingra un nombre para la categoría")]
        [Display(Name ="Nombre Categoría")]
        [Column(TypeName = "varchar(50)")]
        public string Nombre{get;set;}

        [Required]
        [Display(Name = "Orden de visualización")]
        [Column(TypeName = "int")]
        public int Orden { get; set; }
    }
}

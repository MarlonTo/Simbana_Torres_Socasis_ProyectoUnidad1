using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product
    {
        [Key] // Indica que es la clave primaria
        public int ProductID { get; set; }

        [Required] // Campo obligatorio
        [MaxLength(100)] // Longitud máxima del campo
        public string ProductName { get; set; }

        [Required] // Campo obligatorio para la relación con Category
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")] // Llave foránea
        public virtual Category Category { get; set; }

        [Required] // Campo obligatorio
        [Column(TypeName = "decimal(10, 2)")] // Tipo de dato decimal con precisión
        public decimal UnitPrice { get; set; }

        [Required] // Campo obligatorio
        public int UnitsInStock { get; set; }
    }
}

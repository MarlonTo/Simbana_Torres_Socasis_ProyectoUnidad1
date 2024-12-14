using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Category
    {
        [Key] // Indica que es la clave primaria
        public int CategoryID { get; set; }

        [Required] // Campo obligatorio
        [MaxLength(100)] // Longitud máxima del campo
        public string CategoryName { get; set; }

        [MaxLength(255)] // Campo opcional, con longitud máxima
        public string Description { get; set; }

        // Relación uno a muchos con Products
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp_Depi.core.DTOs
{
    public class CategoryDTo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Is Required")]
        public string Name { get; set; } = string.Empty;

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOR.Model.ViewModels
{
    public class FoodViewModel
    {
        public Guid FoodId { get; set; }
        public virtual ICollection<MenuFoodViewModel> MenuFoods { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public string Description { get; set; }
    }
}

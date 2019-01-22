using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOR.Model
{
    public class Food
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FoodId { get; set; }
        public virtual ICollection<MenuFood> MenuFoods { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
    }
}

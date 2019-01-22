using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOR.Model
{
    public class MenuFood
    {
        public Guid MenuFoodId { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual Guid MenuId { get; set; }
        public virtual Food Food { get; set; }
        public virtual Guid FoodId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOR.Model.ViewModels
{
    public class MenuViewModel
    {
        public Guid MenuId { get; set; }
        public virtual ICollection<MenuFoodViewModel> MenuFoods { get; set; }
        public string Title { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOR.Model
{
    public class Menu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MenuId { get; set; }
        public virtual ICollection<MenuFood> MenuFoods { get; set; }
        public string Title { get; set; }

    }
}

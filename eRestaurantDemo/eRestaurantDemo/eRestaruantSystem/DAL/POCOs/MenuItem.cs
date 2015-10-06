using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region addtional namespaces

#endregion

namespace eRestaurantSystem.DAL.POCOs
{
    public class MenuItems
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? Calories { get; set; }
        public string Comment { get; set; }
    }
}

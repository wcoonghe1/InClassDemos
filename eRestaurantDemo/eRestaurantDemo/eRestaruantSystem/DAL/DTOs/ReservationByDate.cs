using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Addtional namespaces
using System.Collections;
#endregion

namespace eRestaurantSystem.DAL.DTOs
{
    public class ReservationByDate
    {
        public string Description { get; set; }
        //variable to hold a coloection of reservation rows
        public IEnumerable Reservations { get; set; }
    }
}

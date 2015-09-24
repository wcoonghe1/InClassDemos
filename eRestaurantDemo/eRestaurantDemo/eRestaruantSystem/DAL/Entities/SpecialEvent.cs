using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Addtional NameSpaceses;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
#endregion

namespace eRestaruantSystem.DAL.Entities
{
    public class SpecialEvent
    {
        [Key]
        [Required(ErrorMessage="An Event code is required, only one Character")]
        [StringLength(1,ErrorMessage="Event Code is only one Character in length")]
        public string EventCode { get; set; }
        [Required(ErrorMessage="Discription is a required field")]
        [StringLength(30,ErrorMessage="Max 30 characters")]
        public string Description { get; set; }

        public bool Active { get; set; }


        //Navigation Virtual Property
        //this is a parent to the resevation table

        public virtual ICollection<Reservation> Reservations { get; set; }
        

        //default values can be set in the class contructor

        public SpecialEvent()
        {
            Active = true;

        }
    }
}

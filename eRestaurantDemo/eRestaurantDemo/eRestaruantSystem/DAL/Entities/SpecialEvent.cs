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
        public string EventCode { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }


        //Navigation Virtual Property
        //this is a parent to the resevation table

        public virtual ICollection<Resevation> Resevations { get; set; }
        
    }
}

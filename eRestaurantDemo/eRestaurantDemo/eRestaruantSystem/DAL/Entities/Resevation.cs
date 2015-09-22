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
    public class Resevation
    {
        [Key]
        public int ResevationID { get; set; }
        public string CustomerName { get; set; }
        public DateTime ResevationDate { get; set; }
        public int NumberInParty { get; set; }
        public string ContactPhone { get; set; }
        public string ResevationStatus { get; set; }
        public string EventCode { get; set; }

        //NAvigation Prperties
        public virtual SpecialEvent Event { get; set; }

        //the Resevations table (sql) is a many to many relationship to the Tables table(sql)
        
        //SQL solves this problem by having an associate table that has a Compound Promary Key Created from
        //Resevatinos and Tables

        //We will not be creating an entity for this associate table
        //insted we will create an overloaded map in our DbContext Class.
        //howeer, we can still create the virtual navigation property to acomodate this relationship

        public virtual ICollection<Table> Tables { get; set; }

    }
}

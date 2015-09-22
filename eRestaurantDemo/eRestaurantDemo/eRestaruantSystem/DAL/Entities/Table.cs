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
    public class Table
    {
        [Key]
        public int TableID { get; set; }
        public byte TableNumber { get; set; } //tiny int in SQL
        public bool Smoking { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }

        //Navigaion Properties

        //the Resevations table (sql) is a many to many relationship to the Tables table(sql)
        
        //SQL solves this problem by having an associate table that has a Compound Promary Key Created from
        //Resevatinos and Tables

        //We will not be creating an entity for this associate table
        //insted we will create an overloaded map in our DbContext Class.
        //howeer, we can still create the virtual navigation property to acomodate this relationship

        public virtual ICollection<Resevation> Resevations { get; set; }
    }
}

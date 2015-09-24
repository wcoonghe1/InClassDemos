using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Addtional NameSpaceses;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using eRestaruantSystem.DAL;
using eRestaruantSystem.DAL.Entities;
using System.ComponentModel;//for the Object Data Sources.
#endregion

namespace eRestaruantSystem.BLL
{
    
    [DataObject]//required for the ODS
    public class AdminController
    {

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SpecialEvent> SpecialEvens_List()
        { 
            //connect  to our DBcontext Class in the DAL
            //Create and instance od the class
            //we will use a transaction to hold our query

            using (var context = new eRestaruantContext())
            { 
                //using method Syntax
                return context.SpecialEvents.OrderBy(x => x.Description).ToList();

            }
        }

    }
}

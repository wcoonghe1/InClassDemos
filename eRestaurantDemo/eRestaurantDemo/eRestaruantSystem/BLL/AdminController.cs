using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Addtional NameSpaceses;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using eRestaurantSystem.DAL;
using eRestaurantSystem.DAL.Entities;
using System.ComponentModel;//for the Object Data Sources.
#endregion

namespace eRestaurantSystem.BLL
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

            using (var context = new eRestaurantContext())
            { 
                //using method Syntax
                //return context.SpecialEvents.OrderBy(x => x.Description).ToList();

                //using query syntax

                var results = from item in context.SpecialEvents
                              orderby item.Description
                              select item;
                return results.ToList();                
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Reservation> GetResevatoinsByEventCode(string eventcode)
        {
            
            using (var context = new eRestaurantContext())
            {
                
                var results = from item in context.Resevatoins
                              where item.EventCode.Equals(eventcode)
                              orderby item.CustomerName, item.ReservationDate
                              select item;
                return results.ToList();

            }
        }

    }
}

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
using eRestaurantSystem.DAL.DTOs;
using eRestaurantSystem.DAL.POCOs;
#endregion

namespace eRestaurantSystem.BLL
{
    
    [DataObject]//required for the ODS
    public class AdminController
    {
        #region Quries
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
                
                var results = from item in context.Reservations
                              where item.EventCode.Equals(eventcode)
                              orderby item.CustomerName, item.ReservationDate
                              select item;
                return results.ToList();

            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ReservationByDate> GetReservationsByDate(string reservationdate)
        {
            using (var context = new eRestaurantContext())
            { 
                //linq is not very playfull or co-op with date time
                //extract the year, month and day our self out of the passed parameter value
                int theYear = (DateTime.Parse(reservationdate)).Year;
                int theMonth = (DateTime.Parse(reservationdate)).Month;
                int theDay = (DateTime.Parse(reservationdate)).Day;

                var results = from eventitem in context.SpecialEvents
                              orderby eventitem.Description
                              select new ReservationByDate() //a new instance for each special event table
                              {
                                  Description = eventitem.Description,
                                  Reservations = from row in eventitem.Reservations
                                                 where row.ReservationDate.Year == theYear
                                                 && row.ReservationDate.Month == theMonth
                                                 && row.ReservationDate.Day == theDay
                                                 select new ReservationDetail() //new instance for each perticular reservatoin
                                                 {
                                                     CustomerName = row.CustomerName,
                                                     ReservationDate = row.ReservationDate,
                                                     NumberInParty = row.NumberInParty,
                                                     ContactPhone = row.ContactPhone,
                                                     ReservationStatus = row.ReservationStatus
                                                 }
                              };
                return results.ToList();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<MenuCategoryItem> MenuCategoryItem_List()
        {
            using (var context = new eRestaurantContext())
            {
                var results = from menuitem in context.MenuCategories
                              orderby menuitem.Description
                              select new MenuCategoryItem 
                              {
                                  Description = menuitem.Description,
                                  MenuItems = from row in menuitem.MenuItems                                                 
                                                 select new MenuItems()
                                                 {
                                                     Description = row.Description,
                                                     Price = row.CurrentPrice,
                                                     Calories = row.Calories,
                                                     Comment = row.Comment
                                                 }
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Waiter> Waiters_List()
        { 
            using (var context = new eRestaurantContext())
            {
                var results = from item in context.Waiters
                              orderby item.LastName, item.FirstName
                              select item;
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Waiter Get_Waiter_By_ID(int waiterid)
        {
            using (var context = new eRestaurantContext())
            {
                var results = from item in context.Waiters
                              where item.WaiterID == waiterid
                              select item;
                return results.FirstOrDefault();//one row at most
            }
        }
        #endregion

        #region Add, Update, Delete of CRUD for CQRS
        //add
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void SpecialEvents_Add(SpecialEvent item)
        {   using (eRestaurantContext context = new eRestaurantContext())
            {
            //these methods are executed using an instant level item.
            //setup a instance pointer and initialize to null
            SpecialEvent added = null;
            //setup the command to execute the add
            added = context.SpecialEvents.Add(item);
            //command is not executed until it is saved
            context.SaveChanges();
            }
        }
        //update
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void SpecialEvents_Update(SpecialEvent item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                context.Entry<SpecialEvent>(context.SpecialEvents.Attach(item)).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        //delete
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void SpecialEvents_Delete(SpecialEvent item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                
                //lookup the instance and record if found(set pointer to instance
                SpecialEvent exising = context.SpecialEvents.Find(item.EventCode);
                //setup the command to execute the delete
                context.SpecialEvents.Remove(exising);
                //command is not executed until it is saved
                context.SaveChanges();
            }
        }

        //for waiter 
        //add
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Waiter_Add(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                //these methods are executed using an instant level item.
                //setup a instance pointer and initialize to null
                Waiter added = null;
                //setup the command to execute the add
                added = context.Waiters.Add(item);
                //command is not executed until it is saved
                context.SaveChanges();
                //the waiter instance added cointains the newly created record to SQL including the generated Primary key value\
                return added.WaiterID;
            }
        }
        //update
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Waiter_Update(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                context.Entry<Waiter>(context.Waiters.Attach(item)).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        //delete
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Waiter_Delete(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {

                //lookup the instance and record if found(set pointer to instance
                Waiter exising = context.Waiters.Find(item.WaiterID);
                //setup the command to execute the delete
                context.Waiters.Remove(exising);
                //command is not executed until it is saved
                context.SaveChanges();
            }
        }
        #endregion


    }//end of class
}//end of namespace

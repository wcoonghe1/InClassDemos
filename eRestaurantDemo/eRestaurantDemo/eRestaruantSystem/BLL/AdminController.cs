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
        #endregion//end of Queires
        #region Reports
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CategoryMenuItems> GetReportCategoryMenuItems()
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var results = from cat in context.Items
                              orderby cat.Category.Description, cat.Description
                              select new CategoryMenuItems
                              {
                                  CategoryDescription = cat.Category.Description,
                                  ItemDescription = cat.Description,
                                  Price = cat.CurrentPrice,
                                  Calories = cat.Calories,
                                  Comment = cat.Comment
                              };

                return results.ToList(); // this was .Dump() in Linqpad
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<WaiterBilling> GetWaiterBillingReport()
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var results = from abillrow in context.Bills
                              where abillrow.BillDate.Month == 5
                              orderby abillrow.BillDate, abillrow.Waiter.LastName, abillrow.Waiter.FirstName
                              select new WaiterBilling()
                              {
                                  BillDate = abillrow.BillDate.Year + "/" + abillrow.BillDate.Month + "/" + abillrow.BillDate.Day,//this removes the Time from datetime, but Linq And SQl dont like to work with Date time so use custom concatination
                                  WaiterName = abillrow.Waiter.LastName + ", " + abillrow.Waiter.FirstName,
                                  BillID = abillrow.BillID,
                                  BillTotal = abillrow.Items.Sum(eachbillitemrow => eachbillitemrow.Quantity * eachbillitemrow.SalePrice),
                                  PartySize = abillrow.NumberInParty,
                                  Contact = abillrow.Reservation.CustomerName
                              };

                return results.ToList(); // this was .Dump() in Linqpad
            }
        }
        #endregion//end of Reports        
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
        #region FrontDesk

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ReservationCollection> ReservationsByTime(DateTime date)
        {
            using (var context = new RestaurantContext())
            {
                var result = (from data in context.Reservations
                              where data.ReservationDate.Year == date.Year
                              && data.ReservationDate.Month == date.Month
                              && data.ReservationDate.Day == date.Day
                                  // && data.ReservationDate.Hour == timeSlot.Hours
                              && data.ReservationStatus == Reservation.Booked
                              select new ReservationSummary()
                              {
                                  ID = data.ReservationID,
                                  Name = data.CustomerName,
                                  Date = data.ReservationDate,
                                  NumberInParty = data.NumberInParty,
                                  Status = data.ReservationStatus,
                                  Event = data.Event.Description,
                                  Contact = data.ContactPhone
                              }).ToList();
                var finalResult = from item in result
                                  orderby item.NumberInParty
                                  group item by item.Date.Hour into itemGroup
                                  select new ReservationCollection()
                                  {
                                      Hour = itemGroup.Key,
                                      Reservations = itemGroup.ToList()
                                  };
                return finalResult.OrderBy(x => x.Hour).ToList();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<SeatingSummary> SeatingByDateTime(DateTime date, TimeSpan time)
        {
            using (var context = new eRestaurantContext())
            {
                // Step 1 - Get the table info along with any walk-in bills and reservation bills for the specific time slot
                var step1 = from data in context.Tables
                            select new
                            {
                                Table = data.TableNumber,
                                Seating = data.Capacity,
                                // This sub-query gets the bills for walk-in customers
                                Bills = from walkIn in data.Bills
                                        where
                                                walkIn.BillDate.Year == date.Year
                                            && walkIn.BillDate.Month == date.Month
                                            && walkIn.BillDate.Day == date.Day
                                            // The following won't work in EF to Entities - it will return this exception:
                                            //  "The specified type member 'TimeOfDay' is not supported..."
                                            // && walkIn.BillDate.TimeOfDay <= time
                                            && DbFunctions.CreateTime(walkIn.BillDate.Hour, walkIn.BillDate.Minute, walkIn.BillDate.Second) <= time
                                            && (!walkIn.OrderPaid.HasValue || walkIn.OrderPaid.Value >= time)
                                        //                          && (!walkIn.PaidStatus || walkIn.OrderPaid >= time)
                                        select walkIn,
                                // This sub-query gets the bills for reservations
                                Reservations = from booking in data.Reservations
                                               from reservationParty in booking.Bills
                                               where
                                                       reservationParty.BillDate.Year == date.Year
                                                   && reservationParty.BillDate.Month == date.Month
                                                   && reservationParty.BillDate.Day == date.Day
                                                   // The following won't work in EF to Entities - it will return this exception:
                                                   //  "The specified type member 'TimeOfDay' is not supported..."
                                                   // && reservationParty.BillDate.TimeOfDay <= time
                                                   && DbFunctions.CreateTime(reservationParty.BillDate.Hour, reservationParty.BillDate.Minute, reservationParty.BillDate.Second) <= time
                                                   && (!reservationParty.OrderPaid.HasValue || reservationParty.OrderPaid.Value >= time)
                                               //                          && (!reservationParty.PaidStatus || reservationParty.OrderPaid >= time)
                                               select reservationParty
                            };
                // Step 2 - Union the walk-in bills and the reservation bills while extracting the relevant bill info
                // .ToList() helps resolve the "Types in Union or Concat are constructed incompatibly" error
                var step2 = from data in step1.ToList() // .ToList() forces the first result set to be in memory
                            select new
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                CommonBilling = from info in data.Bills.Union(data.Reservations)
                                                select new // info
                                                {
                                                    BillID = info.BillID,
                                                    BillTotal = info.Items.Sum(bi => bi.Quantity * bi.SalePrice),
                                                    Waiter = info.Waiter.FirstName,
                                                    Reservation = info.Reservation
                                                }
                            };
                // Step 3 - Get just the first CommonBilling item
                //         (presumes no overlaps can occur - i.e., two groups at the same table at the same time)
                var step3 = from data in step2.ToList()
                            select new
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                Taken = data.CommonBilling.Count() > 0,
                                // .FirstOrDefault() is effectively "flattening" my collection of 1 item into a 
                                // single object whose properties I can get in step 4 using the dot (.) operator
                                CommonBilling = data.CommonBilling.FirstOrDefault()
                            };
                // Step 4 - Build our intended seating summary info
                var step4 = from data in step3
                            select new SeatingSummary()
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                Taken = data.Taken,
                                // use a ternary expression to conditionally get the bill id (if it exists)
                                BillID = data.Taken ?               // if(data.Taken)
                                            data.CommonBilling.BillID  // value to use if true
                                        :                            // else
                                            (int?)null,               // value to use if false
                                // Note: going back to step 2 to be more selective of my Billing Information
                                BillTotal = data.Taken ?
                                            data.CommonBilling.BillTotal : (decimal?)null,
                                Waiter = data.Taken ? data.CommonBilling.Waiter : (string)null,
                                ReservationName = data.Taken ?
                                                    (data.CommonBilling.Reservation != null ?
                                                    data.CommonBilling.Reservation.CustomerName : (string)null)
                                                : (string)null
                            };
                return step4.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public DateTime GetLastBillDateTime()
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var result = context.Bills.Max(eachBillrow => eachBillrow.BillDate);
                return result;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SeatingSummary> AvailableSeatingByDateTime (DateTime Date, TimeSpan Time)
        {
            var results = from seats in SeatingByDateTime(Date, Time)
                          where !seats.Taken
                          select seats;
            return results.ToList();
        }


        public void SeatCustomer(DateTime when, byte tablenumber, int numberinparty, int waiterID)
        { 
                //buisness logic checking sould be done before atampting to place the record on the DB.
            //rule 1 - is the seat abailable
            //rule2 - is the selected table capacity sufficiant
            // get the available seats
            var availabeseats = AvailableSeatingByDateTime(when.Date, when.Time)

                using (eRestaurantContext context = new eRestaurantContext)
                {
                    //creaste a holding list for possinle Business logic errors
                    //this is need for the Messege User Control
                    List<string> errors = new List<string>();
                    if (!availabeseats.Exists(foreachseat => foreachseat.Table == tablenumber))
                    {
                            errors.Add("Table is currently not available");
                    }
                    else if(!availabeseats.Exists(foreachseat => foreachseat.Table == tablenumber && foreachseat.Seating >= numberinparty))
                    {
                        errors.Add("Insufficient seating capacity")
                    }
                    //chcek if any erros excits
                    if(errors.Count > 0)
                    {
                        //throw an exception so the trasnaction exits
                        throw new BusinessRuleException("Unable to seat Customer",errors);
                    }

                    //assume the data is valid
                    //creat a instance of Bill Entity and fill with data
                    Bill seatedcustomer = new Bill();
                    seatedcustomer.BillDate = when;
                    seatedcustomer.NumberInParty = numberinparty;
                    seatedcustomer.WaiterID = waiterID;
                    seatedcustomer.TableID  = tablenumber;

                    context.Bills.Add(seatedcustomer);
                    context.SaveChanges();
                }//edn of transaction
        }
        #endregion
    }//end of class
}//end of namespace

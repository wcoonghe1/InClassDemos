﻿using System;
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

    }
}

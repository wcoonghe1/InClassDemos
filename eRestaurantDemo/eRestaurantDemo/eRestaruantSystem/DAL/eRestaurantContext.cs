using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Addtional NameSpaceses;
using eRestaurantSystem.DAL.Entities;
using System.Data.Entity;
#endregion
// this calss should only be accesible from classes inside this component library



namespace eRestaurantSystem.DAL
{
    //this class will inherit from DBcontext (entity Framework)
    class eRestaurantContext : DbContext
    {

        //create a constructor which will pass the connection string name to the DBcontext.
        public eRestaurantContext() :base("name=EatIn")
        {
            
        }

        //setup of mapping DbSet<T> propeties
        //map an entity to a datebase table
        public DbSet<SpecialEvent> SpecialEvents { get; set; }
        public DbSet<Reservation> Reservatoins { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillItem> BillItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<Waiter> Waiters { get; set; }


        //when overriding the OnModleCreating() method, its important to remember to call the base 
        //method's implimentation
        //before you exit the method.

        //the ManyToManyNavigatoinPropertyConfiguratoin.Map method lets you configure
        //the tables and colums used for this many to many relationship
        //it takes ManyToManyNavigatoinPropertyConfiguratoin.Map instance in which you specify the coloum name
        //by calling the MapLeftKey, MapKeyRight and ToTable Methos

        //the "left" key is the one specified in the HasMany method
        //the "Right" is the one specified in the WithMany method

        //THis navigation replases the SQL accociated table thatg breaks up a many to many relationship.
        //this technique should only be used if the associated table in SQL has only a compund primary kwy and non-key atributs


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Reservation>().HasMany(r => r.Tables)
                .WithMany(t => t.Resevrations)
                .Map(mapping =>
                    {
                        mapping.ToTable("ReservationsTables");
                        mapping.MapLeftKey("ReservationID");
                        mapping.MapRightKey("TabelID");
                    }
                );
            base.OnModelCreating(modelBuilder); //DO NOT REMOVE
        }

    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToursApp.Models
{
    internal class ApplicationDbContext  :DbContext
    {
       
        private static ApplicationDbContext _context;
        public ApplicationDbContext()
        {
        }
        public static ApplicationDbContext GetContext()
        {
            if (_context == null)
            
                _context = new ApplicationDbContext();

            return _context; 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelOfTour>().HasKey(x => new { x.TourId , x.HotelId});

            modelBuilder.Entity<HotelOfTour>()
            .HasOne(sc => sc.Hotel)
            .WithMany(s => s.HotelOfTours)
            .HasForeignKey(sc => sc.HotelId);

            modelBuilder.Entity<HotelOfTour>()
            .HasOne(sc => sc.Tour)
            .WithMany(s => s.HotelOfTours)
            .HasForeignKey(sc => sc.TourId);


            modelBuilder.Entity<TypeOfTour>().HasKey(x => new { x.TourId, x.TypeId });
            modelBuilder.Entity<TypeOfTour>()
           .HasOne(sc => sc.Type)
           .WithMany(s => s.TypeOfTours)
           .HasForeignKey(sc => sc.TypeId);

            modelBuilder.Entity<TypeOfTour>()
            .HasOne(sc => sc.Tour)
            .WithMany(s => s.TypeOfTours)
            .HasForeignKey(sc => sc.TourId);

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-30H4NNH\SQLEXPRESS;Initial Catalog=UhPrakt4;Integrated Security=True");
        }

        public  DbSet<Country> Countries { get; set; }
       public  DbSet<Hotel> Hotels { get; set; }
       public  DbSet<HotelComment> HotelComments { get; set; }
       public  DbSet<HotelImage> HotelImages { get; set; }
       public  DbSet<HotelOfTour> HotelOfTours { get; set; }
       public  DbSet<Tour> Tours { get; set; }
       public  DbSet<Type> Types { get; set; }
       public  DbSet<TypeOfTour> TypeOfTours { get; set; }
       
    }
}

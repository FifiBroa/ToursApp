using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToursApp.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<HotelOfTour> HotelOfTours { get; set; }
        public Country Country { get; set; }
        public int  CountOfStars { get; set; }
        public Hotel()
        {
            HotelOfTours = new List<HotelOfTour>();
        }
    }
}

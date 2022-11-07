using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToursApp.Models
{
    public class HotelOfTour
    { 
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        
     }
}

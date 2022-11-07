using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToursApp.Models
{
    public class Tour
    {
        
        public int Id { get; set; }
        public int TicketCount { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] ImagePreview { get; set; }
        public List<HotelOfTour> HotelOfTours { get; set; }
        public List<TypeOfTour> TypeOfTours { get; set; }
        public decimal Price { get; set; }
        public bool IsActual { get; set; }


        public string ActualText
        {
            get
            {
                return (IsActual) ? "Актуален" : "Завершен";
            }
        }

        public Tour()
        {
            HotelOfTours = new List<HotelOfTour>();
            TypeOfTours = new List<TypeOfTour>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToursApp.Models
{
    public class TypeOfTour
    {
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
    }
}

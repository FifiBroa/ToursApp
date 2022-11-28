using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ToursApp.Models
{
    public class HotelImage
    {
        public int Id { get; set;}
        public Hotel Hotel { get; set; }
        public Byte ImageSource { get; set; }
    }
}

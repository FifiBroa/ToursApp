using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToursApp.Models
{
    public class HotelComment
    {
        public int Id { get; set; }
        public Hotel hotel { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime CratiopnDate { get; set; }
    }
}

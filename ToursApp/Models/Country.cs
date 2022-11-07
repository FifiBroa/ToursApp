using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToursApp.Models
{
    public class Country
    {
        [Key]
        public int Code { get; set; }
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson20App.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int PIN { get; set; }


        public IEnumerable<Payment> Payments { set; get; }
    }
}

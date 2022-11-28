using Lesson20App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson20App
{
    internal class Payment
    {
        public int Id { get; set; }
        public User UserId { get; set; }
        public Category CategoryId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public int Num { get; set; }
        public decimal Price { get; set; }

       
    }
}

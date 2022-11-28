using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson20App.Models
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-30H4NNH\SQLEXPRESS;Initial Catalog=Lesson20;Integrated Security=True");
            
        }
        public DbSet<User>Users { get; set; }
        public DbSet<Payment>Payments { get; set; }
        public DbSet<Category>Categories { get; set; }
    }
}

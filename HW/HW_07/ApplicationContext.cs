using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HW_07.Program;

namespace HW_07
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }
        internal DbSet<Users> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted });
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-V6G1V7P;Database=Shop;Trusted_Connection=True;TrustServerCertificate=True;"); // ПК
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}

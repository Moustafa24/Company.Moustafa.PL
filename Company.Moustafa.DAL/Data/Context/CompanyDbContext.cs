using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Company.Moustafa.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Moustafa.DAL.Data.Context
{
    internal class CompanyDbContext : DbContext
    {



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = . ; Database = CompanyMoustafa ; Trusted_Connection = True ; TrustServerCertificate = True");
        }


        public DbSet<Department> Departments { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Moustafa.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Moustafa.DAL.Data.Configrations
{
    public class EmployeeConfigration : IEntityTypeConfiguration<Employee>
    {
       
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E=>E.Salary).HasColumnType("decimal(18,2)");
        }
    }
}

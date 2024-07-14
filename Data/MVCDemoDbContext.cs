using CrudOperation.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CrudOperation.Data
{
	public class MVCDemoDbContext : DbContext
	{
		public MVCDemoDbContext(DbContextOptions options) : base(options)
		{
		}

        public DbSet<Employee> Employees { get; set; } // Table name Employees is created. 
    }
}
    
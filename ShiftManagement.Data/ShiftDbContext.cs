using Microsoft.EntityFrameworkCore;
using ShiftManagement.Data.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ShiftManagement.Data
{
    public class ShiftDbContext : DbContext
    {
        public ShiftDbContext(DbContextOptions<ShiftDbContext> options) : base(options)
        {
        }

        public DbSet<USR_Users> USR_Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Clients> Clients { get; set; }
		public DbSet<Employees> Employees { get; set; }
        public DbSet<ShiftType> ShiftType { get; set; }
        public DbSet<ShiftDetail> ShiftDetail { get; set; }


    }
}

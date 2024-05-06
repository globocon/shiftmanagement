using Microsoft.EntityFrameworkCore;
using ShiftManagement.Data.Models;

namespace ShiftManagement.Data
{
    public class ShiftDbContext : DbContext
    {
        public ShiftDbContext(DbContextOptions<ShiftDbContext> options) : base(options)
        {

        }

        public DbSet<USR_Users> USR_Users { get; set; }
		public DbSet<Clients> Clients { get; set; }
		public DbSet<Employees> Employees { get; set; }





		public DbSet<DepartmentMaster> DepartmentMaster { get; set; }
        public DbSet<SiteMaster> SiteMaster { get; set; }
        public DbSet<ApplicationMaster> ApplicationMaster { get; set; }
        public DbSet<FieldTypeMaster> FieldTypeMaster { get; set; }
        public DbSet<FieldDetails> FieldDetails { get; set; }
        public DbSet<ModuleOne> Tbl_ModuleOne { get; set; }
        public DbSet<ModuleTwo> Tbl_ModuleTwo { get; set; }

        public DbSet<NewRequestHeader> MstctsRequestHeader { get; set; }
        public DbSet<NewRequestDetail> MstctsRequestDetail { get; set; }

        public DbSet<MissionDetailsHeader> MRO_MissionHeader { get; set; }


    }
}

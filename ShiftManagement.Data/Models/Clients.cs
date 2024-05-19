﻿

namespace ShiftManagement.Data.Models
{
    public class Clients
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public string? Emails { get; set; }
		public string? Phone { get; set; }
		public string? Address { get; set; }
		public int Status { get; set; }
		public DateTime? CreationDate { get; set; }
		public DateTime? DeletionDate { get; set; }
		public bool IsDeleted { get; set; }
        public string? ImageExtn { get; set; }
        public Guid? CompanyId { get; set; }

    }

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Emails { get; set; }
        public string? Address { get; set; }
        public int Status { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? ImageExtn { get; set; }
        public Guid? CompanyId { get; set; }

    }
}

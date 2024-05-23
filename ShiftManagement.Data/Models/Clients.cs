

using System.ComponentModel;

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

    public class PublicClient
    {
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Email")]
        public string? Emails { get; set; }

        [DisplayName("Phone")]
        public string Phone { get; set; }

        [DisplayName("Address")]
        public string? Address { get; set; }   

        public Guid CompanyId { get; set; }

    }
}



namespace ShiftManagement.Data.Models
{

	public class Clients
	{
		public int Id { get; set; }
		public string? Salutation { get; set; }
		public string FirstName { get; set; }
		public string? SecondName { get; set; }
		public string? LastName { get; set; }
		public string? DisplayName { get; set; }
		public string? Gender { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? Address { get; set; }
		public string? UnitOrApartmentNo { get; set; }
		public string? Mobile { get; set; }
		public string? Phone { get; set; }
		public string? Email { get; set; }	
		public string? MaritalStatus { get; set; }
		public string? Nationality { get; set; }
		public string? Languages { get; set; }
		public bool ClientStatus { get; set; }
		public int Status { get; set; }
		public DateTime? CreationDate { get; set; }
		public DateTime? DeletionDate { get; set; }
		public bool IsDeleted { get; set; }
		public string? ImageExtn { get; set; }
		public Guid? CompanyId { get; set; }

	}
}

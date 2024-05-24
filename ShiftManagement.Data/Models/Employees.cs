
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftManagement.Data.Models
{
    public class Employees
    {	
	
        public int Id { get; set; }
		public string? Salutation { get; set; }
		public string Name { get; set; }
		
		public string Email { get; set; }
		public string Mobile { get; set; }
		public string? Phone { get; set; }
        public string? Gender { get; set; }
        public int EmployementTypeId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DOJ { get; set; }
		public string? Address { get; set; }
		public DateTime? CreatedDateTime { get; set; }
        public string? ImageExtn { get; set; }

        public Guid? CompanyId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletionDate { get; set; }

        [NotMapped]
        [DisplayName("Date Of Birth")]
        public string FormattedDOB { get { return DOB.HasValue ? DOB.Value.ToString("dd-MMM-yyyy") : string.Empty; } }
        [NotMapped]
        [DisplayName("Date Of Join")]
        public string FormattedDOJ { get { return DOJ.HasValue ? DOJ.Value.ToString("dd-MMM-yyyy") : string.Empty; } }
        [NotMapped]
        [DisplayName("Gender")]
        public string GenderDesc
        {
            get
            {
                if (Gender != string.Empty && Gender != null)
                {
                    if (Gender.Equals("M"))
                        return "Male";
                    else if (Gender.Equals("F"))
                        return "Female";
                    else return "Not Available";
                }
                else
                {
                    return Gender;
                }
            }
        }

        [NotMapped]
        [DisplayName("Employement Type")]
        public string EmployementTypeDesc
        {
            get
            {
                if (EmployementTypeId.Equals(0))
                    return "Regular";
                else if (EmployementTypeId.Equals(1))
                    return "Part Time";
                else if (EmployementTypeId.Equals(2))
                    return "Contract";
                else if (EmployementTypeId.Equals(3))
                    return "Hourly";
                else return "Not Available";
            }
        }

    }

    public class PublicEmployee
    {
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Phone")]
        public string Phone { get; set; }
        [DisplayName("Gender")]
        public string Gender { get; set; }
        public int EmployementTypeId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DOJ { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string? ImageExtn { get; set; }
        public Guid CompanyId { get; set; }

    }
}

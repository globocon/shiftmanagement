using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftManagement.Data.Models
{
    public class PublicClientEmployeeRequest
    {
        [Key]
        public int RequestId { get; set; }
        public string RequestType { get; set; } // C --> Client, E --> Employee
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public int? EmployementTypeId { get; set; }

        [DataType(DataType.Date)] 
        public DateTime? DOB { get; set; }

        [DataType(DataType.Date)]     
        public DateTime? DOJ { get; set; }
        public string? ImageExtn { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime? RequestDateTime { get; set; }
        public Guid? CompanyId { get; set; }
        public string RequestStatus { get; set; } = "PENDING";
        public int? New_Emp_Client_Id { get; set; }
    }
}

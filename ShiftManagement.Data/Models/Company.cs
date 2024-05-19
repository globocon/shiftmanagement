using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftManagement.Data.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Company ID")]
        public string CompanyNameID { get; set; }
        public bool IsMasterCompany { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public DateTime? CreationDate { get; set; }
        public Guid CreatedUserID { get; set; }
        public DateTime? DeletionDate { get; set; }
        public Guid? DeleteUserID { get; set; }
        public string? ImageExtn { get; set; }
    }
}

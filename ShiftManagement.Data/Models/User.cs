using ShiftManagement.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ShiftManagement.Data.Models
{
    public class User_Login
    {        
        [Required]
        [Display(Name = "Login ID")]
        public string LoginID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
    }

    public class USR_Users
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Login ID")]
        public string LoginID { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public RoleType RoleType { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? ActivateDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public string? ImageExtn { get; set; }
    }

    public class UserSignUp
    {
        
        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string EmailLogin { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
               
    }
}

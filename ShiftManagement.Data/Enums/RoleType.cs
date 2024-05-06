

using System.ComponentModel.DataAnnotations;

namespace ShiftManagement.Data.Enums
{
    public enum RoleType
    {
        [Display(Name = "Super admin")]
        SAdmin = -1,

        [Display(Name = "Company Admin")]
        CAdmin = 0,

        [Display(Name = "Staff")]
        Staff = 1
    }
}

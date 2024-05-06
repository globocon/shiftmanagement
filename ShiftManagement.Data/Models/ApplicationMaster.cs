using System.ComponentModel.DataAnnotations;

namespace ShiftManagement.Data.Models
{
    public class ApplicationMaster
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationDescription { get; set; }
    }
}

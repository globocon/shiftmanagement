using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftManagement.Data.Models
{
    public class ShiftType
    {
        [Key]
        public int Id { get; set; }
        public string ShiftTypeName { get; set;}
        public bool IsDeleted { get; set; }
    }
    public class ShiftDetail
    {
        [Key]
        public int Id { get; set; }
        public int CalanderId { get; set; } = 0;
        public int EmployeeId { get; set; }
        public DateTime ShiftDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ShiftTypeID { get; set; }
        public int ClientId { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public Guid CreateUserId { get; set; }

        [ForeignKey("ShiftTypeID")]
        public ShiftType ShiftType { get; set;}
        [ForeignKey("ClientId")]
        public Clients Clients { get; set; }
        [ForeignKey("EmployeeId")]
        public Employees Employees { get; set; }

        [NotMapped]
        public string formattedStartTime { get { return StartTime != null ? StartTime.ToString("dd-MMM-yyyy HH:mm:ss") : string.Empty; } }

        [NotMapped]
        public string formattedEndTime { get { return EndTime != null ? EndTime.ToString("dd-MMM-yyyy HH:mm:ss") : string.Empty; } }
        [NotMapped]
        public string formattedShiftDate { get { return ShiftDate != null ? ShiftDate.ToString("dd-MMM-yyyy") : string.Empty; } }

    }
}

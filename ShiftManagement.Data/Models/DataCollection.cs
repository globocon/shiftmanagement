using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShiftManagement.Data.Models
{
    internal class DataCollection
    {

    }
}



namespace ShiftManagement.Data.Models
{
    public class MissionMaster
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DivisionId { get; set; }
        public string CotravellerId { get; set; }
        public string Title { get; set; }
        public DateTime MissionStartDate { get; set; }
        public DateTime MissionEndDate { get; set; }
        public DateTime TravelStartDate { get; set; }
        public DateTime TravelEndDate { get; set; }
        public string Location { get; set; }
        public string ActivityType { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftManagement.Data.Models
{
    public class MissionDetailsDetail
    {
        [Key]
        public int MissionDetailId { get; set; }
        [ForeignKey("Id")]
        public int MissionHeaderId { get; set; }
        public string DetailType { get; set; }
        public string DetailTypeId { get; set; }
        public string DetailTypeText { get; set; }
        public MissionDetailsHeader MissionDetailsHeader { get; set; }

    }
}

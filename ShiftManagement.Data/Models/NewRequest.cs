using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftManagement.Data.Models
{
    public class NewRequestHeader
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RequestId { get; set; }
        public string? RequestNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReceivedOnDate { get; set; }
        public int? TypeOfReqId { get; set; }
        public bool LldcCountry { get; set; }
        public int? SubProgrammeId { get; set; }
        public string? StaffResponsibleId { get; set; }

        [MaxLength(200, ErrorMessage = "Exceeded 200 characters")]
        public string? StaffResponsible { get; set; }
        public string? RequestFrom { get; set; }
        public string? RequestTo { get; set; }
        public string? RequestSummary { get; set; }
        public bool Responded { get; set; }
        public int? RespondedThroughId { get; set; }
        public int? RespondedYear { get; set; }
        public string? RespondedDetails { get; set; }
        public int? RequestCreateUserID { get; set; }
        public int? RespondedUserID { get; set; }
        public DateTime? RespondedDateTime { get; set; }

        [NotMapped]
        public virtual ICollection<NewRequestDetail> NewRequestDetail { get; set; } = new List<NewRequestDetail>();

        [NotMapped]
        public string FormattedReceivedOnDate { get { return ReceivedOnDate.HasValue ? ReceivedOnDate.Value.ToString("dd-MMM-yyyy") : string.Empty; } }

    }


    public class NewRequestDetail
    {
        [Key]
        public int DetailId { get; set; }        
        public string? CountryID { get; set; }

        [MaxLength(200, ErrorMessage = "Exceeded 200 characters")]
        public string CountryName { get; set; }

        [ForeignKey("RequestId")]
        public int RequestId { get; set; }

        [NotMapped]
        public virtual NewRequestHeader NewRequestHeader { get; set; }

    }

}
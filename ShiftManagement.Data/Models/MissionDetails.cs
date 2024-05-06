using System.ComponentModel.DataAnnotations;

namespace ShiftManagement.Data.Models
{
    public class MissionDetailsHeader
    {
        [Key]
        public int Id { get; set; }
        public int? SubmittedById { get; set; }
        public int? DivisionId { get; set; }
        public string? MissionTitle { get; set; } 

        [DataType(DataType.Date)]
        public DateTime? MissionStartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? MissionEndDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? TravelStartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? TravelEndDate { get; set; }
        [StringLength(20)]
        public string? LocationLongitude { get; set; } 
        [StringLength(20)]
        public string? LocationLatitude { get; set; }
		public string? LocationAddress { get; set; } 
		public int? ActivityTypeId { get; set; }
        [Required]
        public bool IsConfidential { get; set; }
        [StringLength(100)]
        public string? CollaborationPartnerName { get; set; }
        [StringLength(100)]
        public string? CollaborationPartnerTitle { get; set; }
        [StringLength(100)]
        public string? CollaborationPartnerOrganization { get; set; }
        public string? CollaborationPartnerAddress { get; set; }
        public string? TargetAudience { get; set; }
        public string? MissionObjective { get; set; }
        public string? MissionResults { get; set; }
        public string? KnowledgeProduct { get; set; }
        public bool ContributeToNewPolicies { get; set; }
        public string? ContributeToNewPoliciesDetail { get; set; } 
		public string? WorkAccomplished { get; set; } 
		public int? CreateUserId { get; set; }

	}
}

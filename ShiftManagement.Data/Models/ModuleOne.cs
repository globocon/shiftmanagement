using System.ComponentModel.DataAnnotations;

namespace ShiftManagement.Data.Models
{
    public class ModuleOne
    {

        [Key]
        public int Id { get; set; }
        public string FieldOne { get; set; }
        public string FieldTwo { get; set; }
        
    }
}

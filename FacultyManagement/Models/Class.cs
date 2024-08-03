using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }

        [Display(Name = "Class Code")]
        [Required(ErrorMessage = "Class Code is required.")]
        [StringLength(10, ErrorMessage = "Class Code cannot exceed 10 characters.")]
        public string ClassCode { get; set; } = string.Empty;

        [Display(Name = "Teacher ID")]
        public int? TeacherId { get; set; } // Nullable if it's not required

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; } // Nullable if not required

        [Display(Name = "Finish Date")]
        [DataType(DataType.Date)]
        public DateTime? FinishDate { get; set; } // Nullable if not required

        [Display(Name = "Class Name")]
        [Required(ErrorMessage = "Class Name is required.")]
        [StringLength(100, ErrorMessage = "Class Name cannot exceed 100 characters.")]
        public string ClassName { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string StudentFName { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string StudentLName { get; set; } = string.Empty;

        [Display(Name = "Student Number")]
        [Required(ErrorMessage = "Student Number is required.")]
        [StringLength(20, ErrorMessage = "Student Number cannot exceed 20 characters.")]
        public string StudentNumber { get; set; } = string.Empty;

        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Enrollment Date is required.")]
        public DateTime EnrolDate { get; set; }
    }
}

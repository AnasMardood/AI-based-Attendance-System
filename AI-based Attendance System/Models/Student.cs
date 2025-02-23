using System.ComponentModel.DataAnnotations;

namespace AI_based_Attendance_System.Models
{
    public class Student
    {
        [Key]
        public int Id {  get; set; }
        public string? Name {  get; set; }
        public string? ImagePath {  get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_based_Attendance_System.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public DateTime AttendanceDate { get; set; } 
    }
}

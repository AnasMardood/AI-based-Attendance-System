using AI_based_Attendance_System.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AI_based_Attendance_System.Dataadd
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Student> students {  get; set; }
        public DbSet<Attendance> Attendances {  get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}

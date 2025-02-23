using AI_based_Attendance_System.Dataadd;
using AI_based_Attendance_System.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AI_based_Attendance_System.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var Students = _context.students.ToList();
            return View(Students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagesFolder = Path.Combine("wwwroot", "images");
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                var imagePath = Path.Combine(imagesFolder, imageFile.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                student.ImagePath = "/images/" + imageFile.FileName;  // relative path
            }
            else
            {
                // If an image is not loaded, set a default value or show an error.
                ModelState.AddModelError("imageFile", "Student photo must be uploaded.");
                return View(student);
            }

            _context.students.Add(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

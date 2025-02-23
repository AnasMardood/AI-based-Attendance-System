using AI_based_Attendance_System.Models;
using Emgu.CV.Structure;
using Emgu.CV;
using Microsoft.AspNetCore.Mvc;
using System;
using AI_based_Attendance_System.Dataadd;
using Emgu.CV.Face;

namespace AI_based_Attendance_System.Controllers
{
    
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private LBPHFaceRecognizer faceRecognizer; // Face recognition object

        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
            faceRecognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 120);
            TrainFaceRecognizer(); // Train the model when the application starts
        }

        public IActionResult Index()
        {
            return View();
        }

        private void TrainFaceRecognizer()
        {
            var trainingImages = new List<Mat>();
            var labels = new List<int>();

            foreach (var student in _context.students)
            {
                var studentImagePath = Path.Combine("wwwroot", student.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(studentImagePath))
                {
                    var img = new Image<Gray, byte>(studentImagePath).Resize(100, 100, Emgu.CV.CvEnum.Inter.Linear);
                    trainingImages.Add(img.Mat);
                    labels.Add(student.Id);
                }
            }

            if (trainingImages.Count > 0)
            {
                faceRecognizer.Train(trainingImages.ToArray(), labels.ToArray());
            }
        }

        [HttpPost]
        public async Task<IActionResult> RecordAttendance([FromBody] ImageRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.ImageData))
            {
                return BadRequest("No valid image sent.");
            }

            if (!request.ImageData.Contains(','))
            {
                return BadRequest("Invalid image format.");
            }

            try
            {
                // Convert image from Base64 to image
                var base64Data = request.ImageData.Split(',')[1];
                var imageBytes = Convert.FromBase64String(base64Data);

                var imagePath = Path.Combine("wwwroot/images", "captured_image.jpg");
                System.IO.File.WriteAllBytes(imagePath, imageBytes);

                var capturedImage = new Image<Gray, byte>(imagePath).Resize(100, 100, Emgu.CV.CvEnum.Inter.Linear);

                var prediction = faceRecognizer.Predict(capturedImage);
                if (prediction.Label != -1 && prediction.Distance < 80) 
                {
                    var student = _context.students.FirstOrDefault(s => s.Id == prediction.Label);
                    if (student != null)
                    {
                        _context.Attendances.Add(new Attendance { StudentId = student.Id, AttendanceDate = DateTime.Now });
                        await _context.SaveChangesAsync();
                        return Ok($"Student attendance has been recorded.: {student.Name}");
                    }
                }

                return BadRequest("Student not identified.");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while processing the image.: {ex.Message}");
            }
        }
    }

    public class ImageRequest
    {
        public string ImageData { get; set; }
    }

}


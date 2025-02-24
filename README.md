# AI-Based Attendance System 🎓📸

This project is an **AI-Based Attendance System** designed to automate the process of recording student attendance using facial recognition technology. The system captures images from a webcam, recognizes faces, and records attendance automatically.

![Home page](https://github.com/user-attachments/assets/21bb83de-a4ea-4c8c-873f-6ece0d5e22dc)

---

## Features ✨

- **Real-Time Face Detection**: Captures images from the webcam in real-time.
- **Face Recognition**: Compares captured images with stored student images.
- **Automatic Attendance Recording**: Records attendance for recognized students.
- **Admin Dashboard**: View and manage student attendance records.

---

## Technologies Used 🛠️

- **Back-end**: `ASP.NET Core MVC`
- **Front-end**: `Razor Pages, Bootstrap`
- **Database**: `SQL Server` with `Entity Framework Core`
- **Face Recognition**: `Emgu.CV` (OpenCV for .NET)
- **Authentication**: `ASP.NET Identity`

---

## How It Works 🚀

1. **Student Registration**:
   - Add students with their images to the database.
   - Images are stored in the `wwwroot/images` folder.
![Create](https://github.com/user-attachments/assets/f399eed1-9f33-4cb4-bd4e-f8bdae3ea032)

2. **Attendance Recording**:
   - The system captures images from the webcam.
   - Detects faces and compares them with stored images.
   - Records attendance for recognized students.
![Camera](https://github.com/user-attachments/assets/51072296-3c48-4df3-82e3-6a5c1262541e)

3. **Admin Dashboard**:
   - View and manage student attendance records.
![Ekran görüntüsü 2025-02-24 005826](https://github.com/user-attachments/assets/fe9ac1fa-06fa-4a17-986a-5733d00c4cd1)

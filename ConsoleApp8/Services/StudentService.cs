using ConsoleApp8.Utilities;

namespace ConsoleApp8.Services;

public class StudentService
{
    ClassRoomService classRoomService = new();

        public void GetAllStudents()
        {
            classRoomService.LoadClassrooms();
            bool hasStudents = false;

            foreach (var classRoom in classRoomService.classRooms)
            {
                if (classRoom.Students.Count > 0)
                {
                    hasStudents = true;

                    foreach (var student in classRoom.Students)
                    {
                        student.StudentInfo();
                    }
                }
            }

            if (!hasStudents)
            {
                Helper.Print("Student list is empty", ConsoleColor.Red);
            }
        }
}

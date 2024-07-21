using ConsoleApp8.Exceptions;
using ConsoleApp8.Models;
using ConsoleApp8.Utilities;
using Newtonsoft.Json;

namespace ConsoleApp8.Services;

public class ClassRoomService
{
    public List<ClassRoom> classRooms;
    private static int id;
    private static int studentId;
    public ClassRoomService()
    {
        classRooms = LoadClassrooms();
        if (classRooms.Count > 0)
        {
            id = classRooms.Max(c => c.Id) + 1;
            studentId = GetMaxStudentId() + 1;
        }
        else
        {
            id = 1;
            studentId = 1;
        }
    }
    public void AddClassroom(ClassRoom classRoom)
    {
        classRoom.Id = id;
        classRooms.Add(classRoom);
        SaveClassrooms();
    }
    private int GetMaxStudentId()
    {
        int maxId = 0;
        foreach (var classroom in classRooms)
        {
            foreach (var student in classroom.Students)
            {
                if (student.Id > maxId)
                {
                    maxId = student.Id;
                }
            }
        }
        return maxId;
    }
    public void AddStudent(Student student, int classroomId)
    {
        var classroom = classRooms.Find(c => c.Id == classroomId);
        if (classroom == null)
        {
            throw new ClassroomNotFoundException($"Classroom with ID {classroomId} not found.");
        }
        if (classroom.Students.Count >= classroom.Limit)
        {
            throw new ClassLimitException("Classroom has reached its limit");
        }
        student.Id = studentId++;
        student.ClassId = classroomId;
        classroom.Students.Add(student);
        SaveClassrooms();
    }
    public Student FindStudentById(int id)
    {
        foreach (var classroom in classRooms)
        {
            var student = classroom.Students.Find(s => s.Id == id);
            if (student != null)
            {
                return student;
            }
        }
        throw new StudentNotFoundException($"Student with {id} id not found");
    }
    public void RemoveStudent(int id)
    {
        foreach (var classroom in classRooms)
        {
            var removedStudent = classroom.Students.Find(s => s.Id == id);
            if (removedStudent != null)
            {
                classroom.Students.Remove(removedStudent);
                SaveClassrooms();
                Helper.Print($"Student {removedStudent.Name} removed successfully.", ConsoleColor.Green);
                return;
            }
        }
        throw new StudentNotFoundException($"Student with {id} id not found");
    }

    public void GetStudentsByClassroom(int classroomId)
    {
        var classRoom = classRooms.Find(c => c.Id == classroomId);
        if (classRoom is null)
        {
            throw new ClassroomNotFoundException("Classroom not found");
        }

        if (classRoom.Students.Count == 0)
        {
            Helper.Print("Student list is empty.", ConsoleColor.Red);
        }
        else
        {
            foreach (var student in classRoom.Students)
            {
                student.StudentInfo();
            }
        }
    }


    public void SaveClassrooms()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "data.json");
        using (StreamWriter sr = new StreamWriter(path))
        {
            string json = JsonConvert.SerializeObject(classRooms);
            sr.WriteLine(json);
        }
    }

    public List<ClassRoom> LoadClassrooms()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "data.json");
        using (StreamReader streamReader = new StreamReader(path))
        {
            string result = streamReader.ReadToEnd();
            classRooms = JsonConvert.DeserializeObject<List<ClassRoom>>(result);
            if (classRooms is null)
            {
                classRooms = new List<ClassRoom>();
            }

            return classRooms;
        }
    }
}

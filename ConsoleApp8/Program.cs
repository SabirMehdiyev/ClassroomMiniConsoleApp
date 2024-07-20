using ConsoleApp8.Enums;
using ConsoleApp8.Exceptions;
using ConsoleApp8.Models;
using ConsoleApp8.Services;
using ConsoleApp8.Utilities;

namespace ConsoleApp8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ClassRoomService classService = new ClassRoomService();
            StudentService studentService = new StudentService();

        UserMenu:
            Helper.Print(
                "1.Create classroom\n" +
                "2.Add student\n" +
                "3.Show all students\n" +
                "4.Get students by classroom\n" +
                "5.Remove student", ConsoleColor.DarkCyan);

            string userCommand = Console.ReadLine();

            switch (userCommand)
            {
                case "1":
                repeatClassName:
                    Helper.Print("Please enter classroom name (Example:PB303):", ConsoleColor.Blue);
                    string className = Console.ReadLine();
                    if (!Helper.IsValidName(className))
                    {
                        Helper.Print("Classroom name must consist of 2 upperase letters and 3 digits", ConsoleColor.Red);
                        goto UserMenu;
                    }
                    var tempClass = classService.classRooms.Find(x => x.Name == className);
                    if (tempClass != null)
                    {
                        Helper.Print("Classroom with this name already exists.Please try again!",ConsoleColor.Red);
                        goto repeatClassName;
                    }
                repeatClassType:
                    Helper.Print("Please select classroom type (Frontend or Backend):", ConsoleColor.Blue);
                    ClassRoomType classRoomType;
                    string type = Console.ReadLine().ToUpper();
                    if (type == "FRONTEND")
                    {
                        classRoomType = ClassRoomType.Frontend;
                    }
                    else if (type == "BACKEND")
                    {
                        classRoomType = ClassRoomType.Backend;
                    }
                    else
                    {
                        Helper.Print("Incorrect type!Please enter 'Frontend' or 'Backend'!", ConsoleColor.Red);
                        goto repeatClassType;
                    }
                    ClassRoom classRoom = new(className, classRoomType);
                    classService.AddClassroom(classRoom);
                    Helper.Print("Classroom added successfully", ConsoleColor.Green);
                    goto UserMenu;
                case "2":
                repeatStudName:
                    Helper.Print("Please enter student's name:", ConsoleColor.Blue);
                    string studName = Console.ReadLine();
                    if (!Helper.IsValidInfo(studName))
                    {
                        Helper.Print("Minimum name length must be 3 characters and name must contain 1 upper case", ConsoleColor.Red);
                        goto repeatStudName;
                    }
                repeatStudSurname:
                    Helper.Print("Please enter student's surname:", ConsoleColor.Blue);
                    string studSurname = Console.ReadLine();
                    if (!Helper.IsValidInfo(studSurname))
                    {
                        Helper.Print("Minimum surname length must be 3 characters and surname must contain 1 upper case", ConsoleColor.Red);
                        goto repeatStudSurname;
                    }
                repeatClassroomID:
                    Helper.Print("Existing classrooms:", ConsoleColor.Cyan);
                    foreach (var classroom in classService.classRooms)
                    {
                        classroom.ClassRoomInfo();
                    }
                    Helper.Print("Please enter classroomID to add student:", ConsoleColor.Blue);
                    if (!int.TryParse(Console.ReadLine(), out int classroomId))
                    {
                        Helper.Print("Please enter a valid classroomID!", ConsoleColor.Red);
                        goto repeatClassroomID;
                    }
                    Student student = new(studName, studSurname);
                    try
                    {
                        classService.AddStudent(student, classroomId);
                    }
                    catch (NotFoundException e)
                    {
                        Helper.Print(e.Message, ConsoleColor.Red);
                        goto UserMenu;
                    }
                    catch (ClassLimitException e)
                    {
                        Helper.Print(e.Message, ConsoleColor.Red);
                        goto UserMenu;
                    }
                    catch (Exception e)
                    {
                        Helper.Print(e.Message, ConsoleColor.Red);
                        goto UserMenu;
                    }
                    Helper.Print("Student successfully added", ConsoleColor.Green);
                    goto UserMenu;
                case "3":
                    studentService.GetAllStudents();
                    goto UserMenu;
                case "4":
                repeatClassID:
                    Helper.Print("Existing classrooms:", ConsoleColor.Cyan);
                    foreach (var classroom in classService.classRooms)
                    {
                        classroom.ClassRoomInfo();
                    }
                    Helper.Print("Please enter class id to get students:", ConsoleColor.Blue);
                    if (!int.TryParse(Console.ReadLine(), out int classId))
                    {
                        Helper.Print("Please enter a valid classroomID!", ConsoleColor.Red);
                        goto repeatClassID;
                    }
                    try
                    {
                        classService.GetStudentsByClassroom(classId);
                    }
                    catch (NotFoundException e)
                    {
                        Helper.Print(e.Message, ConsoleColor.Red);
                        goto UserMenu;
                    }
                    goto UserMenu;
                case "5":
                repeatStudentID:
                    Helper.Print("Existing students:",ConsoleColor.Cyan);
                    studentService.GetAllStudents();
                    Helper.Print("Please enter studentId to remove:", ConsoleColor.Blue);
                    if (!int.TryParse(Console.ReadLine(), out int studentId))
                    {
                        Helper.Print("Please enter a valid studentId!", ConsoleColor.Red);
                        goto repeatStudentID;
                    }
                    try
                    {
                        classService.RemoveStudent(studentId);
                    }
                    catch (NotFoundException e)
                    {
                        Helper.Print(e.Message, ConsoleColor.Red);
                        goto UserMenu;
                    }
                    Helper.Print("Student successfully removed", ConsoleColor.Yellow);
                    goto UserMenu;
                default:
                    Helper.Print("Please enter valid command", ConsoleColor.Red);
                    goto UserMenu;
            }

        }


    }
}

using ConsoleApp8.Utilities;

namespace ConsoleApp8.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int ClassId { get; set; }
    public Student(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    public void StudentInfo()
    {
        Helper.Print("-----------------------------------------------------------------------------------------------", ConsoleColor.White);
        Helper.Print($"Id:{Id} - Name:{Name} - Surname:{Surname} - ClassID: {ClassId}", ConsoleColor.White);
    }
}

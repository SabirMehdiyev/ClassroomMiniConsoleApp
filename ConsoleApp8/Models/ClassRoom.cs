using ConsoleApp8.Enums;
using ConsoleApp8.Utilities;

namespace ConsoleApp8.Models;

public class ClassRoom
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Student> Students;
    public ClassRoomType Type { get; set; }
    public int Limit { get; set; }

    public ClassRoom(string name , ClassRoomType type)
    {
        Name = name;
        Type = type;
        Limit = (int)type;
        Students = new();
    }

    public void ClassRoomInfo()
    {
        string typeName = "";
        if (Type == ClassRoomType.Backend)
        {
            typeName = "Backend";
        }
        else if (Type == ClassRoomType.Frontend)
        {
            typeName = "Frontend";
        }
        Helper.Print("-------------------------------------------------------------------------------------------", ConsoleColor.White);
        Helper.Print($"ID: {Id}, Name: {Name}, Type: {typeName}, Limit: {Limit}, Students Count: {Students.Count}",ConsoleColor.White);

    }





}

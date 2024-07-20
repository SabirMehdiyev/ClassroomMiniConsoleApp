namespace ConsoleApp8.Utilities;

public static class Helper
{
    public static void Print(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public static bool IsValidName(this string name)
    {
        if (name.Length != 5)
        {
            return false;
        }
        if (char.IsUpper(name[0]) && char.IsUpper(name[1]) && char.IsDigit(name[2]) && char.IsDigit(name[3]) && char.IsDigit(name[4]))
        {
            return true;
        }
        return false;
    }

    public static bool IsValidInfo(this string info) 
    {
        if (info.Length < 3)
        {
            return false;
        }
        if (!char.IsUpper(info[0]) || info.Trim().Contains(" ") ) 
        {
            return false;
        }
        return true;
    }
}

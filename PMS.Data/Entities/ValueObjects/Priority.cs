public class Priority
{
    public string Name { get; private set; }
    public int Level { get; private set; }
    public string ColorCode { get; private set; }

    public Priority(string name, int level, string colorCode)
    {
        Name = name;
        Level = level;
        ColorCode = colorCode;
    }
    public static Priority High = new Priority("High", 3, "Red");
    public static Priority Medium = new Priority("Medium", 2, "Yellow");
    public static Priority Low = new Priority("Low", 1, "Green");
}

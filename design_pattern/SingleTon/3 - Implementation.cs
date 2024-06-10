public class Singleton
{
    // Hold an instance of the class
    private static Singleton instance;

    // Private constructor ensures that the class cannot be instantiated from outside
    private Singleton() { }

    // Provide a global point to access the instance
    public static Singleton Instance
    {
        get
        {
            // If the instance is null, create a new one
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }

    // Example method to demonstrate its usage
    public void DisplayMessage()
    {
        Console.WriteLine("Singleton Instance");
    }
}

class Program
{
    static void Main()
    {
        Singleton.Instance.DisplayMessage();
    }
}

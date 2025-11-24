namespace TB_CLI;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hi! I am here to help you reorganize your directories and either delete them or move them.");
        Console.WriteLine("What directory would you like to reorganize? Please give me the path to it.");

        Console.ReadLine();
        ExpectedArguments pathArguments = new ExpectedArguments
        {
            Name = "path",
            Alias = "p",
        };

        ArgumentParser argumentParser = new ArgumentParser();
    }
}
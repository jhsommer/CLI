namespace TB_CLI;
class Program
{
    static void Main(string[] args)
    {
        ExpectedArguments pathArguments = new ExpectedArguments
        {
            Name = "path",
            Alias = "p",
        };

        ArgumentParser argumentParser = new ArgumentParser();
        Actions possibleActions = new Actions();
        
        Console.WriteLine("Hi! I am here to help you reorganize your directories and either delete them or move them.");
        Console.WriteLine("What directory would you like to reorganize? Please give me the path to it.");

        string? argument = Console.ReadLine();
        if (argumentParser.CheckPath(argument))
        {
            possibleActions.ScanFiles(argument);
            possibleActions.FilterService();
            
            //Console.WriteLine("This path exists on your disk. What would you like to do?");
        }
    }
}
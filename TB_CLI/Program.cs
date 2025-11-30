namespace TB_CLI;
class Program
{
    static void Main(string[] args)
    {
        ExpectedArguments helpArguments = new ExpectedArguments
        {
            Name = "help",
            Alias = "h",
        };
        
        ExpectedArguments pathArguments = new ExpectedArguments
        {
            Name = "path",
            Alias = "p"
        };

        ExpectedArguments scanArgument = new ExpectedArguments
        {
            Name = "scan",
            Alias = "s"
        };

        ExpectedArguments filterArgument = new ExpectedArguments
        {
            Name = "filter",
            Alias = "f"
        };

        ExpectedArguments showListArgument = new ExpectedArguments
        {
            Name = "showList",
            Alias = "sl"
        };

        ExpectedArguments moveArgument = new ExpectedArguments
        {
            Name = "move",
            Alias = "m"
        };

        ExpectedArguments deleteArgument = new ExpectedArguments
        {
            Name = "delete",
            Alias = "d"
        };

        ArgumentParser argumentParser = new ArgumentParser();
        
        argumentParser.AddExpectedArguments(helpArguments);
        argumentParser.AddExpectedArguments(pathArguments);
        argumentParser.AddExpectedArguments(scanArgument);
        argumentParser.AddExpectedArguments(filterArgument);
        argumentParser.AddExpectedArguments(showListArgument);
        argumentParser.AddExpectedArguments(moveArgument);
        argumentParser.AddExpectedArguments(deleteArgument);
        
        argumentParser.Parse(args);
    }
}
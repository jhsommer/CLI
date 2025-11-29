namespace TB_CLI;

public class ArgumentParser
{
    private const char ParameterDelimiter = '-';
    private List<ExpectedArguments> _expectedArgumentsList = [];

    private const string HelpFile = "help.txt";
    
    

    public void AddExpectedArguments(ExpectedArguments argument)
    {
        this._expectedArgumentsList.Add(argument);
        File.
    }

    public void Parse(string[] args)
    {
        if (args.Length == 0)
        {
            
#if DEBUG
            if (!File.Exists(HelpFile))
            {
                Console.WriteLine("Help file not found.");
                return;
            }
#endif
            
            string helpText = File.ReadAllText(HelpFile);
            Console.WriteLine(helpText);
        }
        
        for (int i = 0; i < args.Length; i++)
        {
            string argument = args[i];
            
            string potentialAlias = argument.TrimStart(ParameterDelimiter);
            foreach (ExpectedArguments expectedArgument in this._expectedArgumentsList)
            {
                if (potentialAlias == expectedArgument.Alias)
                {
                    if (potentialAlias == "p")
                    {
                        GetFollowingPath(expectedArgument, args, i);
                    }

                    if (potentialAlias == "h")
                    {
                        string helpText = File.ReadAllText(HelpFile);
                        Console.WriteLine(helpText);
                    }
                    
                    
                    
                }
            }
            
            string potentialFullName =  potentialAlias.TrimStart(ParameterDelimiter);
            foreach (ExpectedArguments expectedArgument in this._expectedArgumentsList)
            {
                if (potentialFullName == expectedArgument.Name)
                {
                    if (potentialFullName == "path")
                    {
                        GetFollowingPath(expectedArgument, args, i);
                    }

                    if (potentialFullName == "help")
                    {
                        string helpText = File.ReadAllText(HelpFile);
                        Console.WriteLine(helpText);
                    }
                    
                }
            }
        }
    }

    private void GetFollowingPath(ExpectedArguments argument, string[] args, int foundIndex)
    {
        if (foundIndex + 1 >= args.Length)
        {
            Console.WriteLine("Please enter a valid path");
            return;
        }
        
        string followingValue = args[foundIndex + 1];
        argument.Value = followingValue;
    }
    
    public bool CheckPath(string args)
    {
            if (Directory.Exists(args))
            {
                return true;
            }
            return false;
    }
}
using TB_CLI.Actions;

namespace TB_CLI;

public class ArgumentParser
{
    private const char ParameterDelimiter = '-';
    private List<ExpectedArguments> _expectedArgumentsList = [];

    private const string HelpFile = "help.txt";
    private string pathFile = "path.txt";
    
    Scan scanAction = new Scan();
    Filter filterAction = new Filter();
    Move moveAction = new Move();
    Delete deleteAction = new Delete();
    
    
    

    public void AddExpectedArguments(ExpectedArguments argument)
    {
        this._expectedArgumentsList.Add(argument);
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
                    if (potentialFullName == "help")
                    {
                        string helpText = File.ReadAllText(HelpFile);
                        Console.WriteLine(helpText);
                    }
                    
                    if (potentialFullName == "path")
                    {
                        GetFollowingPath(expectedArgument, args, i);
                    }

                    if (potentialFullName == "scan")
                    {
                        if(!string.IsNullOrWhiteSpace(pathFile))
                        {
                            string pathText = File.ReadAllText(pathFile);
                            scanAction.ScanFiles(pathText);
                        }
                        else
                            Console.WriteLine("No path to scan. Please first use the path command.");
                    }

                    if (potentialFullName == "filter")
                    {
                        if (GetFollowingNumber(expectedArgument, args, i) == -1)
                        {
                            return;
                        }
                        
                        filterAction.FilterService(scanAction.files, GetFollowingNumber(expectedArgument, args, i));
                    }

                    if (potentialFullName == "move")
                    {
                        GetFollowingPath(expectedArgument, args, i);
                        moveAction.MoveAction(expectedArgument.Value);
                    }

                    if (potentialFullName == "delete")
                    {
                        deleteAction.DeleteAction();
                    }
                    
                }
            }
        }
    }

    private void GetFollowingPath(ExpectedArguments argument, string[] args, int foundIndex)
    {
        if (foundIndex + 1 >= args.Length)
        {
            Console.WriteLine("Please enter a valid path to a directory.");
            return;
        }
        
        string followingValue = args[foundIndex + 1];
        
        if(CheckPath(followingValue))
        {
            argument.Value = followingValue;
            File.WriteAllText(pathFile, argument.Value);
        }
    }
    
    public bool CheckPath(string args)
    {
            if (Directory.Exists(args))
            {
                return true;
            }
            return false;
    }

    private int GetFollowingNumber(ExpectedArguments argument, string[] args, int foundIndex)
    {
        string followingValue = args[foundIndex + 1];

        if (int.TryParse(followingValue, out int number) && number <= 0)
        {
            Console.WriteLine("Please enter a valid number to filter by in weeks.");
            return -1;
        }

        return number;
        
    }
}
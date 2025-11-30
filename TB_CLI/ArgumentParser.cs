using TB_CLI.Actions;

namespace TB_CLI;

public class ArgumentParser
{
    private const char ParameterDelimiter = '-';
    private readonly List<ExpectedArguments> _expectedArgumentsList = [];

    private const string HelpFile = "help.txt";
    private const string PathFile = "path.txt";

    private readonly Scan _scanAction = new();
    private readonly Filter _filterAction = new();
    private readonly Move _moveAction = new();
    private readonly Delete _deleteAction = new();
    private readonly ShowList _showListAction = new();
    
    public void AddExpectedArguments(ExpectedArguments argument)
    {
        this._expectedArgumentsList.Add(argument);
    }

    public void Parse(string[] args)
    {
        if (args.Length == 0)
        {
            string helpText = File.ReadAllText(HelpFile);
            Console.WriteLine(helpText);
            return;
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
                        return;
                    }

                    if (potentialAlias == "h")
                    {
                        ShowHelp();
                        return;
                    }

                    if (potentialAlias == "s")
                    {
                        StartScan();
                        return;
                    }

                    if (potentialAlias == "f")
                    {
                        StartFilter(args, expectedArgument, i);
                        return;
                    }

                    if (potentialAlias == "sl")
                    {
                        _showListAction.Action();
                        return;
                    }

                    if (potentialAlias == "m")
                    {
                        StartMove(args, expectedArgument, i);
                        return;
                    }

                    if (potentialAlias == "d")
                    {
                        StartDelete();
                        return;
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
                        ShowHelp();
                        return;
                    }
                    
                    if (potentialFullName == "path")
                    {
                        GetFollowingPath(expectedArgument, args, i);
                        return;
                    }

                    if (potentialFullName == "scan")
                    {
                        StartScan();
                        return;
                    }

                    if (potentialFullName == "filter")
                    {
                        StartFilter(args, expectedArgument, i);
                        return;
                    }

                    if (potentialFullName == "showList")
                    {
                        _showListAction.Action();
                        return;
                    }

                    if (potentialFullName == "move")
                    {
                        StartMove(args, expectedArgument, i);
                        return;
                    }

                    if (potentialFullName == "delete")
                    {
                        StartDelete();
                        return;
                    }
                    
                }
            }
        }
        
        Console.WriteLine("No valid command found.");
        ShowHelp();
    }
    private static void ShowHelp()
    {
        string helpText = File.ReadAllText(HelpFile);
        Console.WriteLine(helpText);
    }
    
    private void GetFollowingPath(ExpectedArguments argument, string[] args, int foundIndex)
    {
        if (foundIndex + 1 >= args.Length)
        {
            Console.WriteLine("Please enter a valid path to a directory.");
            File.WriteAllText("path.txt", " ");
            return;
        }
        
        string followingValue = args[foundIndex + 1];
        
        if(CheckPath(followingValue))
        {
            argument.Value = followingValue;
            File.WriteAllText(PathFile, argument.Value);
        }
        else
        {
            File.WriteAllText("path.txt", " ");
        }
    }
    public bool CheckPath(string args)
    {
        if (args == "C:\\" || args == "/" || args == "D:\\")
        {
            Console.WriteLine("I am a coward and do not operate directly on a root directory.");
            return false;
        }
            
        if (Directory.Exists(args))
        {
            return true;
        }
        return false;
    }
    private void StartScan()
    {
        if(!File.Exists(PathFile))
        {
            Console.WriteLine("Please use first the path command.");
            ShowHelp();
            return;
        }
        
        string pathText = File.ReadAllText(PathFile);

        if (string.IsNullOrWhiteSpace(pathText))
        {
            Console.WriteLine("No path to scan. Please first use the path command.");
            return;
        }

        _scanAction.ScanFiles(pathText);
    }
    private void StartFilter(string[] args, ExpectedArguments expectedArgument, int i)
    {
        if (GetFollowingNumber(expectedArgument, args, i) == -1)
        {
            Console.WriteLine("Please enter a valid number to filter by in weeks.");
            return;
        }
                        
        _filterAction.FilterService(GetFollowingNumber(expectedArgument, args, i));
    }
    private int GetFollowingNumber(ExpectedArguments argument, string[] args, int foundIndex)
    {
        string followingValue = args[foundIndex + 1];

        if (int.TryParse(followingValue, out int number) && number <= 0)
        {
            return -1;
        }

        return number;
    }
    private void StartMove(string[] args, ExpectedArguments expectedArgument, int i)
    {
        GetFollowingPath(expectedArgument, args, i);
                        
        string pathText = File.ReadAllText(PathFile);
        _moveAction.MoveAction(pathText);
    }
    private void StartDelete()
    {
        _deleteAction.DeleteAction();
    }

}
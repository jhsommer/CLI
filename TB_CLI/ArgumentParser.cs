namespace TB_CLI;

public class ArgumentParser
{
    private List<ExpectedArguments> expectedArgumentsList;

    public void AddExpectedArguments(ExpectedArguments argument)
    {
        this.expectedArgumentsList.Add(argument);
    }
    
    public void ParseService(string[] args)
    {
        
    }
}
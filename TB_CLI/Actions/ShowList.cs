namespace TB_CLI.Actions;

public class ShowList
{
    private List<string> Load(string content)
    {
        if (!File.Exists(content))
        {
            return new List<string>();
        }
        
        return File.ReadAllLines("lists.txt").ToList();  
    }
    
    public void Action()
    {
        List<string> filteredFiles = Load("lists.txt");

        foreach (string file in filteredFiles)
        {
            Console.WriteLine(file);
        }
    }
}
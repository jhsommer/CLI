namespace TB_CLI.Actions;

public class Move
{
    private static List<string> Load(string content)
    {
        return File.ReadAllLines("lists.txt").ToList();  
    }
    
    
    private static string content = File.ReadAllText("lists.txt");
    
    private List<string> filteredFiles = Load(content);
    
    public void MoveAction(string newPath)
    {
        foreach (string file in filteredFiles)
        {
            File.Move(file, newPath);
        }
    }
}
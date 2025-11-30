namespace TB_CLI.Actions;

public class Move
{
    private List<string> Load(string content)
    {
        if (!File.Exists(content))
        {
            return new List<string>();
        }
        
        return File.ReadAllLines("lists.txt").ToList();  
    }
    
    public void MoveAction(string newPath)
    {
        List<string> filteredFiles = Load("lists.txt");
        
        foreach (string file in filteredFiles)
        {
            File.Move(file, newPath);
        }
    }
}
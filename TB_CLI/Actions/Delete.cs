namespace TB_CLI.Actions;

public class Delete
{
    private static List<string> Load(string content)
    {
        if (!File.Exists(content))
        {
            return new List<string>();
        }
        
        return File.ReadAllLines("lists.txt").ToList();  
    }
    
    public void DeleteAction()
    {
        List<string> filteredFiles = Load("lists.txt");
        
        foreach (string file in filteredFiles)
        {
            File.Delete(file);  
        }
    }
}
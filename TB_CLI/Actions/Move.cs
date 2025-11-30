namespace TB_CLI.Actions;

public class Move
{
    private List<string> Load(string content)
    {
        if (!File.Exists(content))
        {
            return new List<string>();
        }
        
        return File.ReadAllLines(content).ToList();  
    }
    
    public void MoveAction(string newPath)
    {
        List<string> filteredFiles = Load("lists.txt");
        
        foreach (string file in filteredFiles)
        {
            string fileName = Path.GetFileName(file);
            string destination =  Path.Combine(newPath, fileName);

            if (File.Exists(destination))
            {
                Console.WriteLine($"Skipping file {fileName} because it already exists");
                continue;
            }
            
            File.Move(file, destination);
        }
    }
}
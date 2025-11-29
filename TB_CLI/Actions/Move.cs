namespace TB_CLI.Actions;

public class Move
{
    public void MoveAction(string newPath)
    {
        if (!Directory.Exists(newPath))
            return;
        
        foreach (string file in filteredFiles)
        {
            File.Move(file, newPath);
        }
    }
}
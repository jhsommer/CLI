namespace TB_CLI.Actions;

public class Delete
{
    public void DeleteAction()
    {
        foreach (string file in filteredFiles)
        {
            File.Delete(file);  
        }
    }
}
namespace TB_CLI.Actions;

public class Scan 
{
    public string[] files = [];
    
    public void ScanFiles(string path)
    {
        files = Directory.GetFiles(path);
    }
}
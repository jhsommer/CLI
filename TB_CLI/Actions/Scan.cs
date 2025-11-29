namespace TB_CLI.Actions;

public class Scan
{
    private string[] files = [];
    
    public void ScanFiles(string path)
    {
        files = Directory.GetFiles(path);
    }
}
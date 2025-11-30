namespace TB_CLI.Actions;

public class Scan 
{
    public string[] files = [];
    
    public void ScanFiles(string path)
    {
        files = Directory.GetFiles(path);

        File.WriteAllLines("unfilteredFiles.txt", files);
        
        Console.WriteLine("Saved scan to " + Path.GetFullPath("unfilteredFiles.txt"));
    }
}
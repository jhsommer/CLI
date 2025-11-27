namespace TB_CLI;

public class Actions
{
    private DateTime now = DateTime.Now;
    private TimeSpan age;
    private int weeks = 15;
    
    private string[] files = [];
    
    private List<string> filteredFiles = new ();
    public void ScanFiles(string path)
    {
        files = Directory.GetFiles(path);
    }
    
    public void FilterService()
    {
        DateTime fileDate = DateTime.UnixEpoch;
        FileAttributes attributes;
        foreach (string file in files)
        {
            fileDate = File.GetLastWriteTime(file);
            attributes = File.GetAttributes(file);
            age = now -  fileDate;
            
            if (age.TotalDays >= weeks * 7)
            {
                if((attributes & FileAttributes.Hidden) != 0 || (attributes & FileAttributes.System) != 0)
                    continue;
                
                filteredFiles.Add(file);
            }
        }

#if DEBUG
        Console.WriteLine("Filtered Files");
        foreach (string file in filteredFiles)
        {
            Console.WriteLine(file);
        }
#endif
        
    }

    public void MoveAction(string newPath)
    {
        if (!Directory.Exists(newPath))
            return;
        
        foreach (string file in filteredFiles)
        {
            File.Move(file, newPath);
        }
    }

    public void DeleteAction()
    {
        foreach (string file in filteredFiles)
        {
          File.Delete(file);  
        }
    }
}
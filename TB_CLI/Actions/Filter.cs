namespace TB_CLI.Actions;

public class Filter
{
    private List<string> Load(string content)
    {
        Console.WriteLine("Loading " + content);
        
        if (!File.Exists(content))
        {
            return new List<string>();
        }
        
        return File.ReadAllLines(content).ToList();  
    }
    
    
    private DateTime now = DateTime.Now;
    private TimeSpan age;
    private List<string> filteredFiles = new ();
    
    public void FilterService( int weeks)
    {
        List<string> unfilteredFiles = Load("unfilteredFiles.txt");
        
        
        Console.WriteLine($"Filtering {unfilteredFiles.Count} files...");

        if (unfilteredFiles.Count == 0)
        {
            Console.WriteLine("No files, please use before the filter command the scan command.");
        }
        
        DateTime fileDate = DateTime.UnixEpoch;
        FileAttributes attributes;
        foreach (string file in unfilteredFiles)
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

        File.WriteAllLines("lists.txt",  filteredFiles);
        Console.WriteLine($"Saved {filteredFiles.Count} filtered files");
        
    }
}
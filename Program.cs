using DevLog.Models;
using DevLog.Repositories;

var repo = new LogRepository();

if (args.Length == 0)
{
    Console.WriteLine("Usage: dotnet run -- add <text> | list | summary");
    return;
}

switch (args[0])
{
    case "add":
        if (args.Length < 2) 
        {
            Console.WriteLine("Usage: dotnet run -- add \"your log text\"");
            return;
        }
        var text = string.Join(" ", args[1..]);
        var entry = new LogEntry(Guid.NewGuid(), DateTime.Now, text);
        repo.Add(entry);
        Console.WriteLine($"✓ Logged: {entry.Text}");
        break;
    
    case "list":
        var entries = repo.GetAll();
        if (entries.Count == 0)
        {
            Console.WriteLine("No entries yet.");
            return;
        }
        foreach (var e in entries)
        {
            Console.WriteLine($"[{e.Date:yyyy-MM-dd HH:mm}] {e.Text}");
        }
        break;
    
    default:
        Console.WriteLine($"Unknown command: {args[0]}");
        break;
}

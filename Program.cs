using System.ComponentModel.DataAnnotations;
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

        var dateArg = args.SkipWhile(e => !e.Equals("--date")).Skip(1).FirstOrDefault();
        bool parsed = DateTime.TryParse(dateArg, out DateTime filterDate);

        if (dateArg != null && parsed)
        {
            entries = entries.Where(e => e.CreatedAt.Date == filterDate.Date).ToList();
        }

        foreach (var e in entries)
        {
            Console.WriteLine($"[{e.CreatedAt:yyyy-MM-dd HH:mm}] {e.Text}");
        }
        break;
    
    case "summary":
        var logEntries = repo.GetAll();
        var numEntries = logEntries.Count;

        Console.WriteLine($"Total entries: {numEntries}");
        Console.WriteLine("Entries per day:");

        var entriesPerday = logEntries.GroupBy(entry => entry.CreatedAt.Date);

        foreach (var e in entriesPerday)
        {
            Console.WriteLine($"{e.Key:yyyy-MM-dd}: {e.Count()}");
        }

        var longestEntry = logEntries.MaxBy(e => e.Text.Length);
        if (longestEntry != null)
        {
            Console.WriteLine($"Longest entry: \"{longestEntry.Text}\"");
        }
        break;
    
    default:
        Console.WriteLine($"Unknown command: {args[0]}");
        break;
}

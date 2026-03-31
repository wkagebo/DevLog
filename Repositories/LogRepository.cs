using System.Text.Json;
using DevLog.Models;

namespace DevLog.Repositories;

public class LogRepository
{
    private readonly string _filePath = "devlog.json";
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public List<LogEntry> GetAll()
    {
        if (!File.Exists(_filePath))
        {
            return new List<LogEntry>();
        }

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<LogEntry>>(json) ?? new List<LogEntry>();
    }

    public void Add(LogEntry entry)
    {
        var entries = GetAll();
        entries.Add(entry);
        var json = JsonSerializer.Serialize(entries, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }
}
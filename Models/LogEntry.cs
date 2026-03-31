namespace DevLog.Models;

public record LogEntry(
    Guid Id,
    DateTime Date,
    string Text
);
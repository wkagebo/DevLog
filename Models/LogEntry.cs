namespace DevLog.Models;

public record LogEntry(
    Guid Id,
    DateTime CreatedAt,
    string Text
);
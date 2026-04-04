using DevLog.Models;

namespace DevLog.Repositories;

public interface ILogRepository
{
    List<LogEntry> GetAll();

    void Add(LogEntry entry);

}
namespace Application.AdventureLogs;

public interface IAdventureLogsService
{
    IEnumerable<AdventureLogRecord> GetCurrentAdventureLog();
}
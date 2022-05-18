namespace Db;

public class AdventureLog
{
    public int Id { get; set; }
    public int AdventureId { get; set; }
    public int AdventureScriptStepId { get; set; }
    public virtual Adventure Adventure { get; set; }
}
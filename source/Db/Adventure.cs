namespace Db;

public class Adventure
{
    public int Id { get; set; }
    public int AdventureScriptId { get; set; }
    public DateTime Started { get; set; }
    public bool IsPending { get; set; }
    public virtual ICollection<AdventureLog> Logs { get; set; }
}
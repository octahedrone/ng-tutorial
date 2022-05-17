namespace Db;

public class AdventureScript
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public virtual ICollection<AdventureScriptStep> AdventureScriptSteps { get; set; }
}
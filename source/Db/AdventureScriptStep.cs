namespace Db;

public class AdventureScriptStep
{
    public int Id { get; set; }
    public int AdventureScriptId { get; set; }
    public int? ParentStepId { get; set; }
    public int OrderNumber { get; set; }
    public string OptionText { get; set; }
    public string Text { get; set; }
    public virtual AdventureScript AdventureScript { get; set; }
    public virtual AdventureScriptStep ParentStep { get; set; }
    public virtual ICollection<AdventureScriptStep> Options { get; set; }
}
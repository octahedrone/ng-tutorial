using Microsoft.EntityFrameworkCore;

namespace Db;

[Index(nameof(AdventureId), nameof(AdventureScriptStepId), Name = "UC_AdventureLog_AdventureId_AdventureScriptStepId", IsUnique = true)]
public class AdventureLog
{
    public int Id { get; set; }
    public int AdventureId { get; set; }
    public int AdventureScriptStepId { get; set; }
    public virtual Adventure Adventure { get; set; }
}
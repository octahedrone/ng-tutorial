using Application.Playground;
using Db;
using Microsoft.EntityFrameworkCore;

namespace Application.ScriptEditor;

public class ScriptEditorService : IScriptEditorService
{
    private readonly AdventureContext _dataContext;
    private readonly IAdventurePlaygroundService _adventureService;

    public ScriptEditorService(AdventureContext dataContext, IAdventurePlaygroundService adventureService)
    {
        _dataContext = dataContext;
        _adventureService = adventureService;
    }

    public bool HasCurrentScript()
    {
        return _dataContext.AdventureScripts.Any();
    }

    public ScriptEditorScript GetCurrentScript()
    {
        var scriptRecord = _dataContext.AdventureScripts
            .Include(x => x.AdventureScriptSteps)
            .SingleOrDefault();
        if (scriptRecord == null)
            return null;

        var scriptStepsRecords = scriptRecord.AdventureScriptSteps
            .Select(x => new
            {
                x.Id,
                x.ParentStepId,
                Record = new ScriptEditorScriptStep
                {
                    Text = x.Text,
                    OptionText = x.OptionText,
                    OrderNumber = x.OrderNumber
                }
            })
            .ToDictionary(x => x.Id);

        var recordsWithParent = scriptStepsRecords.Values.Where(x => x.ParentStepId != null);
        foreach (var child in recordsWithParent)
        {
            var parent = scriptStepsRecords[child.ParentStepId.Value];
            if (parent.Record.Options == null)
            {
                parent.Record.Options = new List<ScriptEditorScriptStep>();
            }
            parent.Record.Options.Add(child.Record);
        }

        return new ScriptEditorScript
        {
            Created = scriptRecord.Created,
            Root = scriptStepsRecords.Values.Single(x => x.ParentStepId == null).Record
        };
    }

    public void ReplaceCurrentScript(ScriptEditorScript script)
    {
        using var transaction = _dataContext.Database.BeginTransaction();

        DeleteCurrentScript();

        transaction.Commit();
    }

    public void DeleteCurrentScript()
    {
        var scriptRecord = _dataContext.AdventureScripts
            .Include(x => x.AdventureScriptSteps)
            .SingleOrDefault();
        
        if (scriptRecord == null)
            return;
        
        _adventureService.DeleteCurrentAdventure();

        _dataContext.AdventureScripts.Remove(scriptRecord);
        _dataContext.SaveChanges();
    }
}
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
        var scriptEntity = _dataContext.AdventureScripts
            .Include(x => x.AdventureScriptSteps)
            .SingleOrDefault();
        
        return scriptEntity.ToEditorScript();
    }

    public void ReplaceCurrentScript(ScriptEditorScript script)
    {
        var scriptEntity = script.ToScriptEntity();
        
        using var transaction = _dataContext.Database.BeginTransaction();

        DeleteCurrentScript();

        _dataContext.AdventureScripts.Add(scriptEntity);
        _dataContext.SaveChanges();

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
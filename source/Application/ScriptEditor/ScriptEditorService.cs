using Db;

namespace Application.ScriptEditor;

public class ScriptEditorService : IScriptEditorService
{
    private readonly AdventureContext _dataContext;

    public ScriptEditorService(AdventureContext dataContext)
    {
        _dataContext = dataContext;
    }

    public bool HasCurrentScript()
    {
        return _dataContext.AdventureScripts.Any();
    }

    public ScriptEditorScript GetCurrentScript()
    {
        throw new NotImplementedException();
    }

    public void ReplaceCurrentScript(ScriptEditorScript script)
    {
        throw new NotImplementedException();
    }
}
namespace Application.ScriptEditor;

public interface IScriptEditorService
{
    public bool HasCurrentScript();
    public ScriptEditorScript? GetCurrentScript();
    public ScriptEditorScript? GetSampleScript();
    public void ReplaceCurrentScript(ScriptEditorScript script);
    void DeleteCurrentScript();
}
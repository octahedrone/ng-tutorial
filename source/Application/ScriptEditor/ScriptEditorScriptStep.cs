namespace Application.ScriptEditor;

public class ScriptEditorScriptStep
{
    public string OptionText { get; set; }
    public string Text { get; set; }
    public int OrderNumber { get; set; }
    public List<ScriptEditorScriptStep> Options { get; set; }
}
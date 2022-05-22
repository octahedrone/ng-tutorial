using System;
using Application.ScriptEditor;
using Microsoft.AspNetCore.Mvc;
using YamlFormatter;

namespace Web.Controllers;

[ApiController]
[Route("script/edit")]
public class ScriptEditorController : ControllerBase
{
    private readonly IScriptEditorService _service;

    public ScriptEditorController(IScriptEditorService service)
    {
        _service = service;
    }

    [HttpGet("")]
    public string GetCurrentScriptYaml()
    {
        var script = _service.GetCurrentScript();
        if (script?.Root == null)
        {
            return String.Empty;
        }

        return YamlConverter.Serialize(script.Root);
    }

    [HttpPost("")]
    public void Save(UpdateScriptRequest request)
    {
        var rootStep = YamlConverter.Deserialize(request.Script);
        _service.ReplaceCurrentScript(new ScriptEditorScript
        {
            Created = DateTime.UtcNow,
            Root = rootStep
        });
    }
    
    public class UpdateScriptRequest
    {
        public string Script { get; set; }
    }
}
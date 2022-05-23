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
    public GetScriptResponse GetCurrentScriptYaml()
    {
        var script = _service.GetCurrentScript();
        if (script?.Root == null)
        {
            return new GetScriptResponse();
        }

        var yaml = YamlConverter.Serialize(script.Root);
        return new GetScriptResponse
        {
            Script = yaml
        };
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

    public class GetScriptResponse
    {
        public string Script { get; set; }
    }

    public class UpdateScriptRequest
    {
        public string Script { get; set; }
    }
}
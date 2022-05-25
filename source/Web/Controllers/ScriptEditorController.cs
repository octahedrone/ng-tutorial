using System;
using Application.ScriptEditor;
using Microsoft.AspNetCore.Mvc;
using YamlFormatter;

namespace Web.Controllers;

[ApiController]
[Route("script")]
public class ScriptEditorController : ControllerBase
{
    private readonly IScriptEditorService _service;

    public ScriptEditorController(IScriptEditorService service)
    {
        _service = service;
    }

    [HttpGet("edit")]
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
    
    [HttpGet("sample")]
    public GetScriptResponse GetSampleScriptYaml()
    {
        var script = _service.GetSampleScript();
        var yaml = YamlConverter.Serialize(script.Root);
        return new GetScriptResponse
        {
            Script = yaml
        };
    }

    [HttpPost("edit")]
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
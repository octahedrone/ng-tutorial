using Application.ScriptEditor;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("script/edit")]
public class ScriptEditorController : ControllerBase
{
    private IScriptEditorService _service;

    public ScriptEditorController(IScriptEditorService service)
    {
        _service = service;
    }
}
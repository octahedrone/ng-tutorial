using System;
using System.Reflection;
using DotNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers;

[ApiController]
[Route("diagnostics")]
public sealed class DiagnosticsController : ControllerBase
{
    private readonly ILogger<DiagnosticsController> _logger;

    public DiagnosticsController(ILogger<DiagnosticsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("datetime")]
    public DateTime DateTime() => Assembly.GetExecutingAssembly().FileInfo().LastWriteTime;

    [HttpPost("error")]
    public IActionResult Error(ErrorLog error)
    {
        _logger.Log(LogLevel.Error, "FE: {@error}", error);
        return Ok();
    }

    public class ErrorLog
    {
        public string Message { get; set; }
        public string Url { get; set; }
    }
}
using System;
using System.Reflection;
using DotNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("diagnostics")]
public sealed class DiagnosticsController : ControllerBase
{
    [HttpGet("datetime")]
    public DateTime DateTime() => Assembly.GetExecutingAssembly().FileInfo().LastWriteTime;
}

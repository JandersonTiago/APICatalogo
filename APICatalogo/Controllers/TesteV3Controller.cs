using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("api/Teste")]
[ApiController]
[ApiVersion(3)]
[ApiVersion(4)]
public class TesteV3Controller : ControllerBase
{
    [HttpGet]
    [MapToApiVersion("3")]
    public string GetVersionV3()
    {
        return "Version3 - GET - API Versão 3.0";
    }
    [HttpGet]
    [MapToApiVersion("4")]
    public string GetVersionV4()
    {
        return "Version4 - GET - API Versão 4.0";
    }
}

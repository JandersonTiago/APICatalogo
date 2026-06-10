using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters;

public class APIExceptionFilter : IExceptionFilter
{
    readonly ILogger<APIExceptionFilter> _logger;

    public APIExceptionFilter(ILogger<APIExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Ocorreu uma exceção não tratada: Status code 500");
        context.Result = new ObjectResult("Ocorreu um problema ao tratar sua solicitação: Status code 500")
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}

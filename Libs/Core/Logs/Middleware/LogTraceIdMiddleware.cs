using Core.TraceLogic.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Core.Logs.Middleware;

public class LogTraceIdMiddleware  
{  
    private readonly RequestDelegate _next;  
  
    public LogTraceIdMiddleware(RequestDelegate next)  
    {  
        _next = next;  
    }  
  
    public async Task InvokeAsync(HttpContext context, IEnumerable<ITraceReader> traceReaderList)  
    {  
        foreach(var traceReader in traceReaderList)  
        {  
            traceReader.WriteValue(context.Request.Headers["TraceId"]);
        }
        await _next(context);
    }
} 
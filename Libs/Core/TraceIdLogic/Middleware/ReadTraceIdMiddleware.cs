using Core.TraceLogic.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Core.TraceIdLogic;

public class ReadTraceIdMiddleware  
{  
    private readonly RequestDelegate _next;  
  
    public ReadTraceIdMiddleware(RequestDelegate next)  
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
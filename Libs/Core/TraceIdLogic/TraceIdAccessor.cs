using Core.TraceLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Serilog.Context;

namespace Core.TraceIdLogic;

public interface ITraceIdAccessor
{
    
}


internal class TraceIdAccessor : ITraceReader, ITraceWriter, ITraceIdAccessor
{
    public string Name => "TraceId";

    private string _value;
    
    public string GetValue()
    {
        return _value;
    }

    public void WriteValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            value = Guid.NewGuid().ToString();
        }
        
        _value = value;
        Log.Information("TraceId");
    }
}
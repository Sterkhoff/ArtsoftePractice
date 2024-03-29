using Core.Connection.HttpLogic.Services.Connection;
using Core.Connection.HttpLogic.Services.Interfaces;
using Core.Connection.HttpLogic.Services.Polly;
using Core.Connection.HttpLogic.Services.Request;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Connection.HttpLogic;

public static class HttpServiceStartUp
{
    public static IServiceCollection AddHttpServiceStartUp(this IServiceCollection services)
    {
        services
            .AddHttpContextAccessor()
            .AddHttpClient()
            .AddTransient<IHttpConnectionService, HttpConnectionService>()
            .AddTransient<IPollyRequestSender, PollyRequestSender>();

        services.AddTransient<IHttpRequestService, HttpRequestService>();
        return services;
    }
}
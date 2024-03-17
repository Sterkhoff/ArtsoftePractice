using Core.HttpLogic.Services.Connection;
using Core.HttpLogic.Services.Interfaces;
using Core.HttpLogic.Services.Polly;
using Core.HttpLogic.Services.Request;
using Microsoft.Extensions.DependencyInjection;

namespace Core.HttpLogic;

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
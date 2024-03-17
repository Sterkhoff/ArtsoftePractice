using Core.HttpLogic.Services.Connection;
using Core.HttpLogic.Services.Request;
using Core.HttpLogic.Services.Response;

namespace Core.HttpLogic.Services.Interfaces;

public interface IHttpRequestService
{
    Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(HttpRequestData requestData, HttpConnectionData connectionData = default);
}
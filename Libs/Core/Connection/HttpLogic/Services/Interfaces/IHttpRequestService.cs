using Core.Connection.HttpLogic.Services.Connection;
using Core.Connection.HttpLogic.Services.Request;
using Core.Connection.HttpLogic.Services.Response;

namespace Core.Connection.HttpLogic.Services.Interfaces;

public interface IHttpRequestService
{
    Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(HttpRequestData requestData, HttpConnectionData connectionData = default);
}
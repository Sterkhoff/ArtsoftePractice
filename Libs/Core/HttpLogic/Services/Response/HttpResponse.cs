using System.Net;

namespace Core.HttpLogic.Services.Response;

public record HttpResponse<TResponse> : BaseHttpResponse
{
    /// <summary>
    /// Тело ответа
    /// </summary>
    public TResponse? Body { get; set; }
}
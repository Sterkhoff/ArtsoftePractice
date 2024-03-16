using System.Net;
using System.Net.Http.Headers;

namespace Core.HttpLogic.Services.Response;

public record BaseHttpResponse
{
    public HttpStatusCode StatusCode { get; set; }
    
    public HttpResponseHeaders? Headers { get; set; }
    
    public HttpContentHeaders? ContentHeaders { get; init; }
    
    public bool IsSuccessStatusCode
    {
        get
        {
            var statusCode = (int)StatusCode;

            return statusCode >= 200 && statusCode <= 299;
        }
    }
}
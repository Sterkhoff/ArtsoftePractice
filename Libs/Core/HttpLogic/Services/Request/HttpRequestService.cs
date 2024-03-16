using System.Net.Mime;
using System.Text;
using Core.HttpLogic.Services.Connection;
using Core.HttpLogic.Services.Interfaces;
using Core.HttpLogic.Services.Response;
using Core.Logs;
using Core.TraceLogic.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Context;

namespace Core.HttpLogic.Services.Request;

/// <inheritdoc />
internal class HttpRequestService : IHttpRequestService
{
    private readonly IHttpConnectionService _httpConnectionService;
    private readonly IEnumerable<ITraceWriter> _traceWriterList;
    private readonly IEnumerable<ITraceReader> _traceReaders;

    ///
    public HttpRequestService(
        IHttpConnectionService httpConnectionService,
        IEnumerable<ITraceWriter> traceWriterList, IEnumerable<ITraceReader> traceReaders)
    {
        _traceReaders = traceReaders;
        _httpConnectionService = httpConnectionService;
        _traceWriterList = traceWriterList;
    }

    /// <inheritdoc />
    public async Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(HttpRequestData requestData,
        HttpConnectionData connectionData)
    {
        var client = _httpConnectionService.CreateHttpClient(connectionData);
        
        var fullUri = requestData.QueryParameterList.Count == 0
            ? requestData.Uri
            : new Uri($"{requestData.Uri.ToString()}?{string.Join("&", requestData.QueryParameterList
                .Select(x => $"{x.Key}={x.Value}"))}");
        
        var httpRequestMessage = new HttpRequestMessage(requestData.Method, fullUri);

        foreach (var traceWriter in _traceWriterList)
        {
            httpRequestMessage.Headers.Add(traceWriter.Name, traceWriter.GetValue());
        }
        
        httpRequestMessage.Content = PrepareContent(requestData.Body, requestData.ContentType);
        var response = requestData.UsePolly ?
            await _httpConnectionService.SendRequestWithPollyAsync(httpRequestMessage, client, default,
                requestData.RequestPollyData) 
            : await _httpConnectionService.SendRequestAsync(httpRequestMessage, client, default);
        return response.IsSuccessStatusCode
            ? new HttpResponse<TResponse>
            {
                Body = JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync()),
                Headers = response.Headers,
                ContentHeaders = response.Content.Headers,
                StatusCode = response.StatusCode
            }
            : new HttpResponse<TResponse>
            {
                Body = default,
                Headers = response.Headers,
                ContentHeaders = response.Content.Headers,
                StatusCode = response.StatusCode
            };
    }

    private static HttpContent PrepareContent(object body, ContentType contentType)
    {
        switch (contentType)
        {
            case ContentType.ApplicationJson:
            {
                if (body is string stringBody)
                {
                    body = JToken.Parse(stringBody);
                }

                var serializeSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };
                var serializedBody = JsonConvert.SerializeObject(body, serializeSettings);
                var content = new StringContent(serializedBody, Encoding.UTF8, MediaTypeNames.Application.Json);
                return content;
            }

            case ContentType.XWwwFormUrlEncoded:
            {
                if (body is not IEnumerable<KeyValuePair<string, string>> list)
                {
                    throw new Exception(
                        $"Body for content type {contentType} must be {typeof(IEnumerable<KeyValuePair<string, string>>).Name}");
                }

                return new FormUrlEncodedContent(list);
            }
            case ContentType.ApplicationXml:
            {
                if (body is not string s)
                {
                    throw new Exception($"Body for content type {contentType} must be XML string");
                }

                return new StringContent(s, Encoding.UTF8, MediaTypeNames.Application.Xml);
            }
            case ContentType.Binary:
            {
                if (body.GetType() != typeof(byte[]))
                {
                    throw new Exception($"Body for content type {contentType} must be {typeof(byte[]).Name}");
                }

                return new ByteArrayContent((byte[])body);
            }
            case ContentType.TextXml:
            {
                if (body is not string s)
                {
                    throw new Exception($"Body for content type {contentType} must be XML string");
                }

                return new StringContent(s, Encoding.UTF8, MediaTypeNames.Text.Xml);
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(contentType), contentType, null);
        }
    }
}
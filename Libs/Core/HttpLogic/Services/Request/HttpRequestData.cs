using System.ComponentModel.DataAnnotations;
using Core.HttpLogic.Services.Polly;

namespace Core.HttpLogic.Services.Request;

public record HttpRequestData
{
    public HttpMethod Method { get; set; }
    
    public Uri Uri { set; get; }

    public object Body { get; set; }
    
    public bool UsePolly { get; set; }
    
    public PollyData RequestPollyData { get; set; }
    
    public ContentType ContentType { get; set; } = ContentType.ApplicationJson;

    public IDictionary<string, string> HeaderDictionary { get; set; } = new Dictionary<string, string>();

    public ICollection<KeyValuePair<string, string>> QueryParameterList { get; set; } =
        new List<KeyValuePair<string, string>>();
}
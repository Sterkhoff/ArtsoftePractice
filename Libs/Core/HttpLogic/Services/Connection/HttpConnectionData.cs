namespace Core.HttpLogic.Services.Connection;

public record struct HttpConnectionData()
{
    public TimeSpan? Timeout { get; set; } = null;
    
    public CancellationToken CancellationToken { get; set; } = default;
    
    public string ClientName { get; set; }
}
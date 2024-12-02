using Zeus.Api.Domain.Common.Services.Enums;
using Zeus.Api.Domain.ServiceAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Domain.Models;

namespace Zeus.Api.Domain.ServiceAggregate;

public abstract class Service : AggregateRoot<ServiceId>
{
    /// <summary>
    /// List of tokens associated with the service.
    /// </summary>
    protected List<ServiceToken> _tokens = [];

    /// <summary>
    /// Type of the service.
    /// </summary>
    /// <example>
    /// ServiceType type = ServiceType.Google;
    /// </example>
    public ServiceType Type { get; }

    /// <summary>
    /// Owner of the service. In other words, the user that created the service.
    /// </summary>
    public UserId OwnerId { get; private set; }

    /// <summary>
    /// Remote client id of the service.
    /// </summary>
    public string ClientId { get; private set; }

    /// <summary>
    /// Public list of tokens associated with the service.
    /// </summary>
    public IReadOnlyList<ServiceToken> Tokens => _tokens.AsReadOnly();
    
    /// <summary>
    /// Adds a token to the service.
    /// </summary>
    /// <param name="token">
    /// Token to add to the service.
    /// </param>
    public void AddToken(ServiceToken token)
    {
        _tokens.Add(token);
    }
    
    /// <summary>
    /// Removes a token from the service.
    /// </summary>
    /// <param name="token">
    /// Token to remove from the service.
    /// </param>
    public void RemoveToken(ServiceToken token)
    {
        _tokens.Remove(token);
    }
    
    /// <summary>
    /// Determines if the service is valid and has at least one token.
    /// </summary>
    public abstract bool IsValid { get; }

    protected Service(ServiceType type, ServiceId id, UserId ownerId, string clientId) : base(id)
    {
        Type = type;
        OwnerId = ownerId;
        ClientId = clientId;
    }
}

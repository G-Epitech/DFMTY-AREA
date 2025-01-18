using MediatR;

using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Infrastructure.Services;

public class DomainEventDelayer
{
    private readonly Queue<IDomainEvent> _events = new();
    private readonly Lock _lock = new();
    private readonly IPublisher _publisher;

    public DomainEventDelayer(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public void DelayEvents(IList<IDomainEvent> domainEvents)
    {
        lock (_lock)
        {
            foreach (var domainEvent in domainEvents)
            {
                _events.Enqueue(domainEvent);
            }
        }
    }

    public async Task PublishDelayedEventsAsync(CancellationToken cancellationToken = default)
    {
        List<IDomainEvent> events;

        lock (_lock)
        {
            events = _events.ToList();
            _events.Clear();
        }

        foreach (var domainEvent in events)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }
    }
}

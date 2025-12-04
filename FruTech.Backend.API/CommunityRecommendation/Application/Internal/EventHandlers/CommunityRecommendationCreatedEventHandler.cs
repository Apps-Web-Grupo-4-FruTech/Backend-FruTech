using FruTech.Backend.API.CommunityRecommendation.Domain.Model.Events;
using FruTech.Backend.API.Shared.Application.Internal.EventHandlers;
using Humanizer;

namespace FruTech.Backend.API.CommunityRecommendation.Application.Internal.EventHandlers;

/// <summary>
/// Event Handler for Community Recommendation Created Events
/// </summary>
public class CommunityRecommendationCreatedEventHandler : IEventHandler<CommunityRecommendationCreatedEvent>
{
    /// <summary>
    ///  Handles the Community Recommendation Created Event
    /// </summary>
    /// <param name="domainEvent"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task Handle(CommunityRecommendationCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    /// <summary>
    ///  Handles the Community Recommendation Created Event
    /// </summary>
    /// <param name="domainEvent"></param>
    /// <returns></returns>
    private static Task On(CommunityRecommendationCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Community Recommendation: {0}", domainEvent.Id.ToString().Humanize());
        return Task.CompletedTask;
    }
}
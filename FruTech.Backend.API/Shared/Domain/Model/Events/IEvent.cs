using Cortex.Mediator.Notifications;

namespace FruTech.Backend.API.Shared.Domain.Model.Events;

/**
 * <summary>
 * Represents a domain event in the system.
 * </summary>
 * <remarks>
 * This interface is used to mark class as a domain event that can be published and handled by event bus.
 * It extends from <see cref="INotification"/> to integrate with the mediator pattern for event handling.
 * </remarks>
 */
public interface IEvent : INotification
{
    
}
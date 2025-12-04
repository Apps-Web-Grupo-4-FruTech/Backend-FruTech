using FruTech.Backend.API.Shared.Domain.Model.Events;
using Cortex.Mediator.Notifications;

namespace FruTech.Backend.API.Shared.Application.Internal.EventHandlers;
/// <summary>
///  Event handler interface
/// </summary>
/// <typeparam name="TEvent"></typeparam>
public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
    
}
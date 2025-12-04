using FruTech.Backend.API.Shared.Domain.Model.Events;

namespace FruTech.Backend.API.CommunityRecommendation.Domain.Model.Events;

public class CommunityRecommendationCreatedEvent(int id, string user, string role, string description) : IEvent
{
    public string User { get; set; }
    public string Role { get; set; }
    public string Description { get; set; }
    public int Id { get; set; }
}
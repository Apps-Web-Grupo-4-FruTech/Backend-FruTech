namespace FruTech.Backend.API.CommunityRecommendation.Domain.Model.Aggregates;

/// <summary>
/// Community Recommendation aggregate root entity
/// </summary>
public partial class CommunityRecommendation
{
    public CommunityRecommendation(int id, string user, string role,
        string description) : this()
    {
        Id = id;
        User = user;
        Role = role;
        Description = description;
    }
    
    public CommunityRecommendation()
    {
        //TODO: Implement community recommendation creation logic
    }
    
    public int Id { get; }
    public string User { get; private  set; }
    
    public string Role { get; private set; }
    
    public string Description { get; private set; }
}
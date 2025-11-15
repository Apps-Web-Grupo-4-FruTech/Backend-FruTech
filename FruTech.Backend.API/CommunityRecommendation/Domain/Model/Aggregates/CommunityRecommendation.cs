namespace FruTech.Backend.API.CommunityRecommendation.Domain.Model.Aggregates;

/// <summary>
/// Community Recommendation aggregate root entity
/// </summary>
public class CommunityRecommendation
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
        User = string.Empty;
        Role = string.Empty;
        Description = string.Empty;
    }
    
    public int Id { get; set; }
    public string User { get; private set; }
    public string Role { get; private set; }
    public string Description { get; private set; }
    
    /// <summary>
    /// Update the community recommendation
    /// </summary>
    public void Update(string role, string description)
    {
        Role = role;
        Description = description;
    }
}
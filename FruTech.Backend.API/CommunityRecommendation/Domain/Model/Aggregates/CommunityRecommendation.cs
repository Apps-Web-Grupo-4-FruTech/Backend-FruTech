using System.Text.Json.Serialization;

namespace FruTech.Backend.API.CommunityRecommendation.Domain.Model.Aggregates;

/// <summary>
/// Recomendación de la comunidad
/// </summary>
public partial class CommunityRecommendation
{
    public int Id { get; set; }
    
    [JsonPropertyName("user_name")]
    public string UserName { get; set; } = string.Empty;
    
    [JsonPropertyName("comment_date")]
    public DateTime CommentDate { get; set; } = DateTime.UtcNow;
    
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
    
    public CommunityRecommendation() { }
    
    public CommunityRecommendation(string userName, string comment)
    {
        UserName = userName;
        Comment = comment;
        CommentDate = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Actualiza la recomendación
    /// </summary>
    public void Update(string userName, string comment)
    {
        UserName = userName;
        Comment = comment;
    }
}
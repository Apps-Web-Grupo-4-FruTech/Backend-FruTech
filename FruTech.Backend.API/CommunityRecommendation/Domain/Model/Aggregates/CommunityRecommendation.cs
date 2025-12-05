using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

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
    
    /// <summary>
    /// Nombre del rol del usuario que hizo la recomendación (no se persiste en la BD).
    /// </summary>
    [NotMapped]
    public string Role { get; set; } = "Sin Asignar";
    
    public CommunityRecommendation() { }
    /// <summary>
    ///  Constructor de la recomendación
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="comment"></param>
    public CommunityRecommendation(string userName, string comment)
    {
        UserName = userName;
        Comment = comment;
        CommentDate = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Actualiza la recomendación completa (usuario y comentario)
    /// </summary>
    public void Update(string userName, string comment)
    {
        UserName = userName;
        Comment = comment;
    }
    
    /// <summary>
    /// Actualiza solo el contenido del comentario y refresca la fecha.
    /// </summary>
    public void UpdateContent(string comment)
    {
        Comment = comment;
        CommentDate = DateTime.UtcNow; // refresca fecha de edición
    }
}
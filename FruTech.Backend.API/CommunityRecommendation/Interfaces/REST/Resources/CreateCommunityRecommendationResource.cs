namespace FruTech.Backend.API.CommunityRecommendation.Interfaces.REST.Resources;

/// <summary>
/// Create community recommendation resource for REST API
/// </summary>
/// <param name="UserName">Nombre del usuario que hace la recomendación</param>
/// <param name="Comment">Comentario de la recomendación</param>
public record CreateCommunityRecommendationResource(string UserName, string Comment);

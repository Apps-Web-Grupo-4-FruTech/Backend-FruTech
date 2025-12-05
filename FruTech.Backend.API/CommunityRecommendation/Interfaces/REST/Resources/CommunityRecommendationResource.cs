namespace FruTech.Backend.API.CommunityRecommendation.Interfaces.REST.Resources;

/// <summary>
///     CommunityRecommendation resource for REST API
/// </summary>
/// <param name="Id">El identificador único de la recomendación</param>
/// <param name="UserName">Nombre del usuario que hizo la recomendación</param>
/// <param name="Role">Nombre del rol del usuario que hizo la recomendación</param>
/// <param name="CommentDate">Fecha del comentario</param>
/// <param name="Comment">Comentario de la recomendación</param>
public record CommunityRecommendationResource(int Id, string UserName, string Role, DateTime CommentDate, string Comment);

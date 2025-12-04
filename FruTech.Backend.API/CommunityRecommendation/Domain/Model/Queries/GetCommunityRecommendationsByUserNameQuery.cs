namespace FruTech.Backend.API.CommunityRecommendation.Domain.Model.Queries;

/// <summary>
/// Query para obtener recomendaciones por nombre de usuario
/// </summary>
public record GetCommunityRecommendationsByUserNameQuery(string UserName);


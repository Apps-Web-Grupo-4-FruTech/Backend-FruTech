namespace FruTech.Backend.API.CommunityRecommendation.Domain.Model.Commands;

/// <summary>
/// Comando para actualizar una recomendación de la comunidad
/// </summary>
public record UpdateCommunityRecommendationCommand(int Id, string UserName, string Comment);

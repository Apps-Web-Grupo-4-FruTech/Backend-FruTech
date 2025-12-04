namespace FruTech.Backend.API.CommunityRecommendation.Domain.Model.Commands;

/// <summary>
/// Comando para crear una nueva recomendación de la comunidad
/// </summary>
public record CreateCommunityRecommendationCommand(string UserName, string Comment);

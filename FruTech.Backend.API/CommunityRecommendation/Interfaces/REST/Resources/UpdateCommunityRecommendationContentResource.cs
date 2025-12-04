// filepath: c:\Users\USER\Desktop\Cosas_Bruce\Ciclo_5.5\APPS WEB\TF\Backen_Apps\FruTech.Backend.API\CommunityRecommendation\Interfaces\REST\Resources\UpdateCommunityRecommendationContentResource.cs
namespace FruTech.Backend.API.CommunityRecommendation.Interfaces.REST.Resources;

/// <summary>
/// Resource para actualizar solo el contenido (comentario) de una recomendación.
/// </summary>
/// <param name="Comment">Nuevo texto del comentario</param>
public record UpdateCommunityRecommendationContentResource(string Comment);


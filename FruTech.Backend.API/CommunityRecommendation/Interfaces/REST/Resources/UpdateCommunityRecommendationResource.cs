﻿namespace FruTech.Backend.API.CommunityRecommendation.Interfaces.REST.Resources;

/// <summary>
/// Resource para actualizar una recomendación de la comunidad
/// </summary>
/// <param name="UserName">Nombre del usuario</param>
/// <param name="Comment">Comentario de la recomendación</param>
public record UpdateCommunityRecommendationResource(string UserName, string Comment);


using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using FruTech.Backend.API.CommunityRecommendation.Domain.Model.Queries;
using FruTech.Backend.API.CommunityRecommendation.Domain.Services;
using FruTech.Backend.API.CommunityRecommendation.Interfaces.REST.Resources;
using FruTech.Backend.API.CommunityRecommendation.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace  FruTech.Backend.API.CommunityRecommendation.Interfaces.REST;

/// <summary>
///     The Community Recommendation controller
/// </summary>
/// <param name="communityRecommendationQueryService">
///     The <see cref="ICommunityRecommendationQueryService"/> community recommendation query service
/// </param>
/// <param name="communityRecommendationCommandService">
///     The <see cref="ICommunityRecommendationCommandService"/> community recommendation command service
/// </param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Community Recommendation Endpoints")]
public class CommunityRecommendationController(
    ICommunityRecommendationQueryService communityRecommendationQueryService,
    ICommunityRecommendationCommandService communityRecommendationCommandService) : ControllerBase
{
    /// <summary>
    ///  Get Community Recommendation by id
    /// </summary>
    /// <param name="RecomendationId">
    ///     The Community Recommendation id
    /// </param>
    /// <returns>
    ///     the community recommendation found
    /// </returns>
    [HttpGet("{recommendationId:int}")]
    [SwaggerOperation(
        Summary = "Get Community Recommendation By Id",
        Description = "Get Community Recommendation By Id",
        OperationId = "GetCommunityRecommendationById")]
    [SwaggerResponse(StatusCodes.Status200OK, "the community recommendation found",
        typeof(CommunityRecommendationResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "the community recommendation not found")]

    public async Task<IActionResult> GetCommunityRecommendationById(int RecomendationId)
    {
        var getRecommendationByIdQuery = new GetCommunityRecommendationByIdQuery(RecomendationId);
        var recommendation = await communityRecommendationQueryService.Handle(getRecommendationByIdQuery);
        if (recommendation is null) return NotFound();
        var resource = CommunityRecommendationResourceFromEntityAssembler.ToResourceFromEntity(recommendation);
        return Ok(resource);
    }

    /// <summary>
    ///     Get all Community Recommendations
    /// </summary>
    /// <returns>
    ///     The list of <see cref="CommunityRecommendationResource"/> community recommendations
    /// </returns> 
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Community Recommendations",
        Description = "Get All Community Recommendations",
        OperationId = "GetAllCommunityRecommendations")]
    [SwaggerResponse(StatusCodes.Status200OK, "the list of community recommendations",
        typeof(IEnumerable<CommunityRecommendationResource>))]
    public async Task<IActionResult> GetAllCommunityRecommendations()
    {
        var recommendations = await communityRecommendationQueryService.Handle(new GetAllCommunityRecommendationsQuery());
        var recommendationResources = recommendations
            .Select(CommunityRecommendationResourceFromEntityAssembler.ToResourceFromEntity)
            .ToList();
        return Ok(recommendationResources);
    }
    
    /// <summary>
    /// Update an existing Community Recommendation
    /// </summary>
    /// <param name="id">
    /// The Community Recommendation id
    /// </param>
    /// <param name="resource">
    /// The <see cref="UpdateCommunityRecommendationResource"/> update community recommendation resource
    /// </param>
    /// <returns>
    /// The <see cref="CommunityRecommendationResource"/> community recommendation updated, or not found if not updated
    /// </returns>
    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update Community Recommendation",
        Description = "Update Community Recommendation",
        OperationId = "UpdateCommunityRecommendation")]
    [SwaggerResponse(StatusCodes.Status200OK, "the updated community recommendation",
        typeof(CommunityRecommendationResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "the community recommendation not found")]
    public async Task<IActionResult> UpdateCommunityRecommendation(int id, [FromBody] UpdateCommunityRecommendationResource resource)
    {
        var command = new FruTech.Backend.API.CommunityRecommendation.Domain.Model.Commands.UpdateCommunityRecommendationCommand(id, resource.UserName, resource.Comment);
        var updated = await communityRecommendationCommandService.Handle(command);
        if (updated is null) return NotFound();
        var updatedResource = CommunityRecommendationResourceFromEntityAssembler.ToResourceFromEntity(updated);
        return Ok(updatedResource);
    }
}
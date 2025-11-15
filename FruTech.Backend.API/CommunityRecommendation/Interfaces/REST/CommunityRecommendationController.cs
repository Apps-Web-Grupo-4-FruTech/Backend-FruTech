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
    /// Create a new Community Recommendation
    /// </summary>
    /// <param name="resource">
    /// The <see cref="CreateCommunityRecommendationResource"/> create community recommendation resource
    /// </param>
    /// <returns>
    /// The <see cref="CommunityRecommendationResource"/> community recommendation created, or bad request if not created
    /// </returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create Community Recommendation",
        Description = "Create Community Recommendation",
        OperationId = "CreateCommunityRecommendation")
    ]
    [SwaggerResponse(StatusCodes.Status201Created, "the community recommendation created",
        typeof(CommunityRecommendationResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "the community recommendation not created")]
    public async Task<IActionResult> CreateCommunityRecommendation(
        [FromBody] CreateCommunityRecommendationResource resource)
    {
        var createRecommendationCommand = CreateCommunityRecommendationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var recommendation = await communityRecommendationCommandService.Handle(createRecommendationCommand);
        if (recommendation is null) return BadRequest();
        var recommendationResource = CommunityRecommendationResourceFromEntityAssembler.ToResourceFromEntity(recommendation);
        return CreatedAtAction(nameof(GetCommunityRecommendationById),
            new { recommendationId = recommendation.Id }, recommendationResource);
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
}
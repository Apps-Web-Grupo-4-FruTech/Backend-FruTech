using Cortex.Mediator;
using FruTech.Backend.API.CommunityRecommendation.Domain.Model.Commands;
using FruTech.Backend.API.CommunityRecommendation.Domain.Repositories;
using FruTech.Backend.API.Shared.Domain.Repositories;
using FruTech.Backend.API.CommunityRecommendation.Domain.Services;
using CommunityRecommendationAggregate = FruTech.Backend.API.CommunityRecommendation.Domain.Model.Aggregates.CommunityRecommendation;

namespace FruTech.Backend.API.CommunityRecommendation.Application.Internal.CommandServices;

/// <summary>
/// Command Service for Community Recommendations
/// </summary>
public class CommunityRecommendationCommandService(
    ICommunityRecommendationRepository communityRecommendationRepository,
    IUnitOfWork unitOfWork) : ICommunityRecommendationCommandService
{
    public async Task<CommunityRecommendationAggregate?> Handle(UpdateCommunityRecommendationCommand command)
    {
        var communityRecommendation = await communityRecommendationRepository.FindByIdAsync(command.Id);
        if (communityRecommendation == null) return null;

        // Update the community recommendation
        communityRecommendation.Update(command.UserName, command.Comment);
        await unitOfWork.CompleteAsync();

        return communityRecommendation;
    }
}

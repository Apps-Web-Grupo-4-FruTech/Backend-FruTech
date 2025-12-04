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
    /// <summary>
    ///  Updates an existing community recommendation
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<CommunityRecommendationAggregate?> Handle(UpdateCommunityRecommendationCommand command)
    {
        var communityRecommendation = await communityRecommendationRepository.FindByIdAsync(command.Id);
        if (communityRecommendation == null) return null;
        communityRecommendation.Update(command.UserName, command.Comment);
        await unitOfWork.CompleteAsync();

        return communityRecommendation;
    }
    /// <summary>
    /// Creates a new community recommendation
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<CommunityRecommendationAggregate> Handle(CreateCommunityRecommendationCommand command)
    {
        var newRecommendation = new CommunityRecommendationAggregate(command.UserName, command.Comment);
        await communityRecommendationRepository.AddAsync(newRecommendation);
        await unitOfWork.CompleteAsync();
        return newRecommendation;
    }
    
    /// <summary>
    ///  Updates only the content of a community recommendation
    /// </summary>
    /// <param name="id"></param>
    /// <param name="newComment"></param>
    /// <returns></returns>
    public async Task<CommunityRecommendationAggregate?> HandleUpdateContent(int id, string newComment)
    {
        var communityRecommendation = await communityRecommendationRepository.FindByIdAsync(id);
        if (communityRecommendation == null) return null;
        communityRecommendation.UpdateContent(newComment);
        await unitOfWork.CompleteAsync();
        return communityRecommendation;
    }
}

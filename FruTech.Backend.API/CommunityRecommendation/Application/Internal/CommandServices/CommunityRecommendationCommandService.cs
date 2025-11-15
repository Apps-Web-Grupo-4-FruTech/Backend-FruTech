using Cortex.Mediator;
using FruTech.Backend.API.CommunityRecommendation.Domain.Model.Commands;
using FruTech.Backend.API.CommunityRecommendation.Domain.Model.Events;
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
    IUnitOfWork unitOfWork,
    IMediator domainEventPublisher) : ICommunityRecommendationCommandService
{
    public async Task<CommunityRecommendationAggregate?> Handle(CreateCommunityRecommendationCommand command)
    {
        var communityRecommendation = new CommunityRecommendationAggregate(
            command.id,
            command.user,
            command.role,
            command.description);

        await communityRecommendationRepository.AddAsync(communityRecommendation);
        await unitOfWork.CompleteAsync();

        // Publish domain event (pass all required parameters)
        await domainEventPublisher.PublishAsync(new CommunityRecommendationCreatedEvent(
            communityRecommendation.Id,
            communityRecommendation.User,
            communityRecommendation.Role,
            communityRecommendation.Description));

        return communityRecommendation;
    }

    public async Task<CommunityRecommendationAggregate?> Handle(UpdateCommunityRecommendationCommand command)
    {
        var communityRecommendation = await communityRecommendationRepository.FindByIdAsync(command.id);
        if (communityRecommendation == null) return null;

        // Update the community recommendation
        communityRecommendation.Update(command.role, command.description);
        
        await unitOfWork.CompleteAsync();

        return communityRecommendation;
    }
}

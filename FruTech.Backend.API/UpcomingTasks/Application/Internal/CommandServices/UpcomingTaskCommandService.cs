using FruTech.Backend.API.Shared.Domain.Repositories;
using FruTech.Backend.API.UpcomingTasks.Domain.Model.Commands;
using FruTech.Backend.API.UpcomingTasks.Domain.Repositories;
using FruTech.Backend.API.UpcomingTasks.Domain.Services;
using UpcomingTaskAggregate = FruTech.Backend.API.UpcomingTasks.Domain.Model.Aggregates.UpcomingTask;

namespace FruTech.Backend.API.UpcomingTasks.Application.Internal.CommandServices;

public class UpcomingTaskCommandService : IUpcomingTaskCommandService
{
    private readonly IUpcomingTaskRepository _upcomingTaskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpcomingTaskCommandService(IUpcomingTaskRepository upcomingTaskRepository, IUnitOfWork unitOfWork)
    {
        _upcomingTaskRepository = upcomingTaskRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpcomingTaskAggregate> Handle(CreateUpcomingTaskCommand command)
    {
        var upcomingTask = new UpcomingTaskAggregate(command.Date, command.Name, command.TaskDescription, command.TaskId);
        await _upcomingTaskRepository.AddAsync(upcomingTask);
        await _unitOfWork.CompleteAsync();
        return upcomingTask;
    }
}


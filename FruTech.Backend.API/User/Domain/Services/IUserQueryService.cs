using FruTech.Backend.API.User.Domain.Model.Queries;
using UserAggregate = FruTech.Backend.API.User.Domain.Model.Aggregates.User;

namespace FruTech.Backend.API.User.Domain.Services;
/// <summary>
///  Service interface for handling user-related queries.
/// </summary>
public interface IUserQueryService
{
    Task<UserAggregate?> Handle(GetUserByIdQuery query);
}

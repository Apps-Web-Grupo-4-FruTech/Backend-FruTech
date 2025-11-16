using UserAggregate = FruTech.Backend.API.User.Domain.Model.Aggregates.User;
using FruTech.Backend.API.User.Interfaces.REST.Resources;

namespace FruTech.Backend.API.User.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(UserAggregate entity)
        => new(entity.Id, entity.UserName, entity.Email, entity.PhoneNumber, entity.Identificator, entity.CreatedDate, entity.UpdatedDate);
}

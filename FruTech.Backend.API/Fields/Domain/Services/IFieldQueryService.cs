using FruTech.Backend.API.Fields.Interfaces.REST.Resources;
using FruTech.Backend.API.Fields.Domain.Model.Queries;

namespace FruTech.Backend.API.Fields.Domain.Services;

/// <summary>
/// Query service for Field
/// </summary>
public interface IFieldQueryService
{
    Task<IEnumerable<FieldResource>> Handle(GetFieldsByUserIdQuery query);
    Task<FieldResource?> Handle(GetFieldByIdQuery query);
}

using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Domain.Model.Queries;

namespace FruTech.Backend.API.Fields.Domain.Services;

/// <summary>
/// Servicio de consultas para Field
/// </summary>
public interface IFieldQueryService
{
    Task<IEnumerable<Field>> Handle(GetFieldsByUserIdQuery query);
    Task<Field?> Handle(GetFieldByIdQuery query);
}


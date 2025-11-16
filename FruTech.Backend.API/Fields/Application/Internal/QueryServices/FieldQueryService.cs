using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Domain.Model.Queries;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Fields.Domain.Services;

namespace FruTech.Backend.API.Fields.Application.Internal.QueryServices;

/// <summary>
/// Servicio de consultas para Field
/// </summary>
public class FieldQueryService : IFieldQueryService
{
    private readonly IFieldRepository _fieldRepository;

    public FieldQueryService(IFieldRepository fieldRepository)
    {
        _fieldRepository = fieldRepository;
    }

    public async Task<IEnumerable<Field>> Handle(GetFieldsByUserIdQuery query)
    {
        return await _fieldRepository.FindByUserIdAsync(query.UserId);
    }

    public async Task<Field?> Handle(GetFieldByIdQuery query)
    {
        return await _fieldRepository.FindByIdAsync(query.FieldId);
    }
}


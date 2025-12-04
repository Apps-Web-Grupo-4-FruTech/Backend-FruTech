using FruTech.Backend.API.Fields.Domain.Model.Queries;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Fields.Domain.Services;
using FruTech.Backend.API.Fields.Interfaces.REST.Resources;
using FruTech.Backend.API.Fields.Interfaces.REST.Transform;

namespace FruTech.Backend.API.Fields.Application.Internal.QueryServices;

/// <summary>
/// Query service for Field
/// </summary>
public class FieldQueryService : IFieldQueryService
{
    /// <summary>
    ///  Field repository
    /// </summary>
    private readonly IFieldRepository _fieldRepository;
    /// <summary>
    ///  Constructor
    /// </summary>
    /// <param name="fieldRepository"></param>
    public FieldQueryService(IFieldRepository fieldRepository)
    {
        _fieldRepository = fieldRepository;
    }
    /// <summary>
    ///  Gets fields by user id
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<IEnumerable<FieldResource>> Handle(GetFieldsByUserIdQuery query)
    {
        var fields = await _fieldRepository.FindByUserIdAsync(query.UserId);
        return fields.Select(f => FieldResourceFromEntityAssembler.ToResource(f));
    }
    /// <summary>
    ///  Gets field by id
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<FieldResource?> Handle(GetFieldByIdQuery query)
    {
        var field = await _fieldRepository.FindByIdAsync(query.FieldId);
        if (field is null) return null;

        return FieldResourceFromEntityAssembler.ToResource(field);
    }
}

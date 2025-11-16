using FruTech.Backend.API.Fields.Domain.Model.Commands;
using FruTech.Backend.API.Fields.Domain.Model.Entities;

namespace FruTech.Backend.API.Fields.Domain.Services;

/// <summary>
/// Servicio de comandos para Field
/// </summary>
public interface IFieldCommandService
{
    Task<Field> Handle(CreateFieldCommand command);
}


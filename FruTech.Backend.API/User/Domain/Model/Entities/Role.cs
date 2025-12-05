namespace FruTech.Backend.API.User.Domain.Model.Entities;

/// <summary>
/// Representa un rol en el sistema.
/// </summary>
public class Role
{
    /// <summary>
    /// Identificador único del rol.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Nombre del rol.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Colección de relaciones con usuarios.
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}



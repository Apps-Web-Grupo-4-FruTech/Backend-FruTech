namespace FruTech.Backend.API.User.Domain.Model.Entities;

/// <summary>
/// Representa la relación entre un usuario y un rol.
/// </summary>
public class UserRole
{
    /// <summary>
    /// Identificador único de la relación.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Identificador del usuario.
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Identificador del rol.
    /// </summary>
    public int RoleId { get; set; }
    
    /// <summary>
    /// Navegación al usuario.
    /// </summary>
    public Aggregates.User User { get; set; } = null!;
    
    /// <summary>
    /// Navegación al rol.
    /// </summary>
    public Role Role { get; set; } = null!;
}


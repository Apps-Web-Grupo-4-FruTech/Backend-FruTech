
namespace FruTech.Backend.API.User.Domain.Model.Aggregates
{  
    /// <summary>
    ///  Representa un usuario en el sistema.
    /// </summary>
    public partial class User
    {
        public int Id { get; private set; }
        public string UserName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string Identificator { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;

        private User() { }
        /// <summary>
        ///  Constructor de la clase User.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="identificator"></param>

        public User(string userName, string email, string phoneNumber, string identificator)
        {
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Identificator = identificator;
        }
        /// <summary>
        ///  Hashea la contraseña del usuario.
        /// </summary>
        /// <param name="password"></param>
        public void HashPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }
        /// <summary>
        ///  Verifica la contraseña del usuario.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool VerifyPassword(string password)
        {
            if (string.IsNullOrEmpty(PasswordHash)) return false;
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
        /// <summary>
        ///  Actualiza el perfil del usuario.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        public void UpdateProfile(string userName, string email, string phoneNumber)
        {
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        /// <summary>
        ///  Cambia la contraseña del usuario.
        /// </summary>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void ChangePassword(string currentPassword, string newPassword)
        {
            if (!VerifyPassword(currentPassword))
                throw new InvalidOperationException("Current password is incorrect");
            HashPassword(newPassword);
        }
    }
}

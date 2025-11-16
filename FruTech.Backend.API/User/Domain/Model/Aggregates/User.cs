
namespace FruTech.Backend.API.User.Domain.Model.Aggregates
{
    public partial class User
    {
        public int Id { get; private set; }
        public string UserName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string Identificator { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;

        private User() { } // Constructor para EF Core

        public User(string userName, string email, string phoneNumber, string identificator)
        {
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Identificator = identificator;
        }

        public void HashPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            if (string.IsNullOrEmpty(PasswordHash)) return false;
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }

        public void UpdateProfile(string userName, string email, string phoneNumber)
        {
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void ChangePassword(string currentPassword, string newPassword)
        {
            if (!VerifyPassword(currentPassword))
                throw new InvalidOperationException("Current password is incorrect");
            HashPassword(newPassword);
        }
    }
}

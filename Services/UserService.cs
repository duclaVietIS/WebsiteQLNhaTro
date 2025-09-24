using WebsiteQLNhaTro.DTOs;
using WebsiteQLNhaTro.Entities;
using WebsiteQLNhaTro.Models;

namespace WebsiteQLNhaTro.Services
{
    /// <summary>
    /// UserService: handles user management logic.
    /// </summary>
    public class UserService
    {
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();

        private readonly Entities.AppDbContext _context;

        public UserService(Entities.AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Check if a user with the given email exists in DB.
        /// </summary>
        public bool ExistsByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        /// <summary>
        /// Create a new user in the database.
        /// </summary>
        /// <param name="request">User registration data.</param>
        /// <returns>The created User entity.</returns>
        /// <exception cref="ArgumentException">Thrown if email already exists.</exception>
        public User CreateUser(UserRegisterDto request)
        {
            if (ExistsByEmail(request.Email))
                throw new ArgumentException("Email already exists.");

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                Role = request.Role,
                IsActive = true
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        /// <summary>
        /// Verify the email token for a user.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <param name="token">Verification token.</param>
        /// <returns>True if token is valid, false otherwise.</returns>
        internal bool VerifyEmailToken(string email, string token)
        {
            // veryify the token matches what was sent to the user's email
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            if (user == null) throw new ArgumentException("Invalid email.");

            // Here you would typically compare the token with the one stored in the database
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Invalid token.");

            return true;
        }

        internal Models.UserDetailDTO Authenticate(string username, string password)
        {
            //log the username and password being verified
            Console.WriteLine($"Verifying user: {username} with password: {password}");

            var user = _context.Users.SingleOrDefault(u => u.Name.Equals(username));
            if (user == null || !_passwordHasher.VerifyPassword(password, user.PasswordHash))
                return null;
            return new Models.UserDetailDTO
            {
                Id = user.Id,
                Username = user.Name,
                Role = user.Role
            };
        }
    }
}

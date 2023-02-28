using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Backend.src.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DatabaseContext _context;
        private DbSet<User> _dbSet { get; }
        private readonly IConfiguration _configuration;

        public AuthService(DatabaseContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public Task<User> AuthenticateUserAsync(string token)
        {
            return Task.FromResult(VerifyToken(token));
        }

        public async Task<string> LogInAsync(string email, string password)
        {
            var found = await _dbSet.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (found == null)
            {
                throw ServiceException.Unauthorized("Credentials are incorrect");
            } else if (!ServiceHash.CompareHashData(password, found.Password, found.Salt))
            {
                throw ServiceException.Unauthorized("Credentials are incorrect");
            }
            Console.WriteLine(found);
            return CreateToken(found);
        }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };
            var appToken = _configuration.GetSection("AppSettings:Token").Value;
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appToken!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private User VerifyToken(string token)
        {
            
            return new User();
        }
    }
}
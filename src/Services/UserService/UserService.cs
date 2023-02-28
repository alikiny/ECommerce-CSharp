
namespace Backend.src.Services.UserService
{
    public class UserService : BaseService<User, UserReadDto, UserCreateDto, UserUpdateDto>, IUserService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
            _context = context;
            _mapper = mapper;
        }

        public new async Task<User> AddOneAsync(UserCreateDto dto)
        {
            ServiceHash.CreateHashData(dto.PasswordRaw, out byte[] passwordHash, out byte[] passwordSalt);
            if (CheckEmail(dto.Email)) throw ServiceException.BadRequest("Email is already taken");
            var entity = _mapper.Map<User>(dto);
            entity.Password = passwordHash;
            entity.Salt = passwordSalt;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        private Boolean CheckEmail(string email)
        {
            var entity = _context.Users.Where(u => u.Email == email);
            if (entity.Count() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
namespace Backend.src.Services.UserService
{
    public class UserService : BaseService<User, UserReadDto, UserCreateDto, UserUpdateDto>, IUserService
    {
        public UserService(IMapper mapper, IUserRepository repository) : base(mapper, repository)
        {
        }

        public new async Task<UserReadDto> AddOneAsync(UserCreateDto dto)
        {
            ServiceHash.CreateHashData(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var entity = _mapper.Map<UserCreateDto, User>(dto);
            entity.Password = passwordHash;
            entity.Salt = passwordSalt;
            await _repository.AddOneAsync(entity);
            return _mapper.Map<User, UserReadDto>(entity);
        }
    }
}
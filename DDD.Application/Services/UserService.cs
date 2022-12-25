using AutoMapper;
using DDD.Application.DTOs.User;
using DDD.Application.Interfaces;
using DDD.Domain.Interfaces;
using DDD.Domain.Models;

namespace DDD.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepositroy, IMapper mapper)
        {
            _userRepository = userRepositroy;
            _mapper = mapper;
        }

        public async Task CreateAsync(UserDto entity)
        {
            var user = _mapper.Map<User>(entity);
            await _userRepository.CreateAsync(user);
        }


        public async Task DeleteAsync(int id)
        {
           await _userRepository.DeleteAsync(id);
        }

        public IQueryable<UserDto> GetAll()
        {
            var users = _mapper.ProjectTo<UserDto>(_userRepository.GetAll());
            return users;
        }

        public UserDto GetById(int id)
        {
            var user = _userRepository.GetById(id); 
            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto> GetByIdAsync(int id)
        { 
           var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<User, UserDto>(user);  
        }

        public int SaveChanges()
        {
            return _userRepository.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
          return await _userRepository.SaveChangesAsync();
        }

        public bool Update(UserDto entity)
        {
           var user = _userRepository.GetById(entity.Id);

            if (user == null)
            {
                return false;
            }

            _mapper.Map(user, entity);
            return _userRepository.Update(user);
        }

        public async Task<UserBaseDto> CreateUserAsync(UserDto entity)
        {
            var user = _mapper.Map<User>(entity);
            await _userRepository.CreateAsync(user);
            entity.Id = user.Id;

            var response = _mapper.Map<User, UserBaseDto>(user);
            return response;
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            return await _userRepository.IsUsernameTakenAsync(username);
        }
        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _userRepository.IsEmailTakenAsync(email);
        }
    }
}

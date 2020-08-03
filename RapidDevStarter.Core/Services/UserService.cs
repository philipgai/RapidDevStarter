using RapidDevStarter.Core.Interfaces.Repos;
using RapidDevStarter.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RapidDevStarter.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }

        public IQueryable<UserModel> Get()
        {
            return _userRepo.Get();
        }

        public IQueryable<UserModel> Get(int key)
        {
            return _userRepo.Get(key);
        }

        public async Task<UserModel> CreateAsync(UserModel userModel)
        {
            return await _userRepo.CreateAsync(userModel);
        }

        public async Task<UserModel> UpdateAsync(int key, UserModel updatedUserModel)
        {
            return await _userRepo.UpdateAsync(key, updatedUserModel);
        }

        public async Task DeleteAsync(int key)
        {
            await _userRepo.DeleteAsync(key);
        }
    }
}
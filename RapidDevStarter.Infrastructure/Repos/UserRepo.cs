using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RapidDevStarter.Core.Exceptions;
using RapidDevStarter.Core.Interfaces.Repos;
using RapidDevStarter.Core.Models;
using RapidDevStarter.Entities.DbContexts;
using RapidDevStarter.Entities.RapidDevStarterEntities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RapidDevStarter.Infrastructure.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly RapidDevStarterDbContextWrapper _rapidDevStarterDbContext;
        private readonly IMapper _mapper;

        public UserRepo(RapidDevStarterDbContextWrapper rapidDevStarterDbContext, IMapper mapper)
        {
            _rapidDevStarterDbContext = rapidDevStarterDbContext ?? throw new ArgumentNullException(nameof(rapidDevStarterDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IQueryable<UserModel> Get()
        {
            return _rapidDevStarterDbContext.User.ProjectTo<UserModel>(_mapper.ConfigurationProvider);
        }

        public IQueryable<UserModel> Get(int key)
        {
            return _rapidDevStarterDbContext.User.Where(user => user.UserKey == key).ProjectTo<UserModel>(_mapper.ConfigurationProvider);
        }

        public async Task<UserModel> CreateAsync(UserModel userModel)
        {
            var userEntity = _mapper.Map<User>(userModel);
            _rapidDevStarterDbContext.User.Add(userEntity);
            await _rapidDevStarterDbContext.SaveChangesAsync();
            var result = _mapper.Map<UserModel>(userEntity);
            return result;
        }

        public async Task<UserModel> UpdateAsync(int key, UserModel updatedUserModel)
        {
            var currUserEntity = await _rapidDevStarterDbContext.User
                .Include(user => user.ContactInfo)
                .SingleOrDefaultAsync(user => user.UserKey == key);

            if (currUserEntity == null)
            {
                throw new EntityNotFoundException();
            }

            _rapidDevStarterDbContext.Entry(currUserEntity).CurrentValues.SetValues(updatedUserModel);

            if (currUserEntity.ContactInfo == null && updatedUserModel.ContactInfo != null) // Create new ContactInfo
            {
                currUserEntity.ContactInfo = _mapper.Map<ContactInfo>(updatedUserModel.ContactInfo);
            }
            else if (currUserEntity.ContactInfo != null && updatedUserModel.ContactInfo != null) // Update current ContactInfo
            {
                _rapidDevStarterDbContext.Entry(currUserEntity.ContactInfo).CurrentValues.SetValues(updatedUserModel.ContactInfo);
            }
            else if (currUserEntity.ContactInfo != null && updatedUserModel.ContactInfo == null) // Delete ContactInfo
            {
                currUserEntity.ContactInfo = null;
            }

            await _rapidDevStarterDbContext.SaveChangesAsync();

            var result = _mapper.Map<UserModel>(currUserEntity);
            return result;
        }

        public async Task DeleteAsync(int key)
        {
            var userEntity = await _rapidDevStarterDbContext.User
                .Include(user => user.ContactInfo)
                .SingleOrDefaultAsync(user => user.UserKey == key);
            if (userEntity == null)
            {
                throw new EntityNotFoundException();
            }
            _rapidDevStarterDbContext.User.Remove(userEntity);
            await _rapidDevStarterDbContext.SaveChangesAsync();
        }
    }
}
﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Youtube.Api.Core.Dto.Entities;
using Youtube.Api.Core.Dto.GatewayResponses.Repositories;
using Youtube.Api.Core.Interfaces.Gateways.Repositories;
using Youtube.Api.Infrastructure.Auth;
using Youtube.Api.Infrastructure.Data.Entities;

namespace Youtube.Api.Infrastructure.Data.EntityFramework.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly YoutubeContext _database;

        public UserRepository(IMapper mapper, YoutubeContext database)
        {
            _mapper = mapper;
            _database = database;
        }

        public CreatedUserResponse Create(UserDto userInfo)
        {
            var user = _mapper.Map<User>(userInfo);
            _database.Users.Add(user);
            _database.SaveChanges();

            return new CreatedUserResponse(user.Id, true);
        }

        public UserDto FindByEmail(string email)
        {
            User desiredUser = _database.Users.Where(x => x.Email == email).FirstOrDefault();

            return _mapper.Map<UserDto>(desiredUser);
        }

        public UserDto FindById(int id)
        {
            return _mapper.Map<UserDto>(_database.Users.Find(id));
        }

        public bool SetUserProfilePicture(int pictureId, int userId)
        {
            User user = _database.Users.Find(userId);
            ProfilePicture picture = _database.ProfilePictures.Find(pictureId);
            if (user == null || picture == null)
            {
                return false;
            }
            user.ProfilePictureId = pictureId;
            _database.Users.Update(user);
            _database.SaveChanges();
            return true;
        }
    }
}

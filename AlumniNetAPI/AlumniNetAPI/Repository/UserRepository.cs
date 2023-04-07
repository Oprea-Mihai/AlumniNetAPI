﻿using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace AlumniNetAPI.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AlumniNetAppContext context) : base(context)
        {
        }

        public async Task<User>GetUserByIdAsync(int id)
        {
            User user = await _dbSet.SingleAsync(u=>u.UserId==id);
            return user;
        }
    }
}

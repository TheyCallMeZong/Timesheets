﻿using System;
using System.Threading.Tasks;
using Timesheets.Models;
using Timesheets.Models.Dto.Authentication;

namespace Timesheets.Data.Interfaces
{
    public interface IRefreshTokenRepo
    {
        Task<JwtRefreshToken> GetTokenFromDB(string token);
        void Add(JwtRefreshToken refreshToken);
    }
}
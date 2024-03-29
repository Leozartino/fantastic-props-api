﻿using Core.Interfaces;
using Core.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FantasticProps.Helpers
{
    public class JwtSetttingsHelper : IJwtSettingsHelper
    {
        public string GenerateJWT(JwtSettings settings, List<Claim> claims)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(settings.Secret);

            SecurityToken token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = settings.Issuer,
                Audience = settings.Audience,
                Expires = DateTime.UtcNow.AddHours(settings.ExpiresInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                                                            SecurityAlgorithms.HmacSha256)
            });

            return tokenHandler.WriteToken(token);
        }
    }
}

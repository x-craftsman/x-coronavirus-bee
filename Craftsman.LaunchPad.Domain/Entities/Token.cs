using Craftsman.Core.DapperExtensions;
using Craftsman.Core.Domain.Entities;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Craftsman.LaunchPad.Domain.Entities
{
    /// <summary>
    /// Token（访问令牌）信息
    /// </summary>
    public class Token : IEntity
    {
        #region  Field
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        [Column("consumer_id")]
        public int ConsumerId { get; set; }
        [Column("access_token")]
        public string AccessToken { get; set; }
        [Column("refresh_token")]
        public string RefreshToken { get; set; }
        [Column("confusion_code")]
        public string ConfusionCode { get; set; }
        [Column("expiration_time")]
        public DateTime ExpirationTime { get; set; }
        [Column("expiration_interval")]
        public int ExpirationInterval { get; set; }
        [Column("created_by")]
        public int CreatedBy { get; set; }
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        [Column("updated_by")]
        public int UpdatedBy { get; set; }
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }

        [NotMapped]
        public bool IsUpdated { get; set; }
        public bool IsTransient()
        {
            return !IsUpdated;
        }
        #endregion  Field

        #region Action

        public static Token GenerateNew(int userId, int consumerId)
        {
            var token = new Token
            {
                UserId = userId,
                ConsumerId = consumerId,
                AccessToken = GeneratedJwtToken(userId, consumerId),
                RefreshToken = GeneratedJwtToken(userId, consumerId),
                ConfusionCode = string.Empty,
                ExpirationTime = DateTime.Now.AddDays(1),
                ExpirationInterval = 1,
                CreatedBy = 0,
                CreateTime = DateTime.Now,
                UpdatedBy = 0,
                UpdateTime = DateTime.Now,
                IsUpdated = false
            };
            return token;
        }

        public void Refresh()
        {
            //Update Token.
            this.AccessToken = GeneratedJwtToken(this.UserId, this.ConsumerId);
            this.RefreshToken = GeneratedJwtToken(this.UserId, this.ConsumerId);
            this.ExpirationTime = DateTime.Now.AddDays(Convert.ToDouble(this.ExpirationInterval));
            this.UpdatedBy = 0;
            this.UpdateTime = DateTime.Now;
            IsUpdated = true;
        }
        public void ExtendExpirationTime()
        {
            this.ExpirationTime = DateTime.Now.AddDays(Convert.ToDouble(this.ExpirationInterval));
            this.UpdatedBy = 0;
            this.UpdateTime = DateTime.Now;
            IsUpdated = true;
        }

        public bool IsAvailabled()
        {
            throw new NotImplementedException();
        }
        private static string GeneratedJwtToken(int userId, int consumerId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Consts.Secret);
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddDays(1);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtClaimTypes.Audience,"api"),
                    new Claim(JwtClaimTypes.Issuer,"QuantumScm"),
                    new Claim(JwtClaimTypes.Id, userId.ToString()),
                    //new Claim(JwtClaimTypes.Name, user.UserName),
                    new Claim(JwtClaimTypes.ClientId, consumerId.ToString()),
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        #endregion Action
    }
}

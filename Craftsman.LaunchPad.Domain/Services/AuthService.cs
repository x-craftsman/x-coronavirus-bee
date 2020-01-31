using Craftsman.Core.Common;
using Craftsman.Core.CustomizeException;
using Craftsman.Core.Domain;
using Craftsman.Core.Domain.Repositories;
using Craftsman.LaunchPad.Domain.AuthCenter;
using Craftsman.LaunchPad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.LaunchPad.Domain.Services
{
    public interface IAuthService : IService
    {
        object GenerateToken(AuthContext context);
    }
    public class AuthService : IAuthService
    {

        private IRepository<Token> _tokenRepo;
        private IRepository<User> _userRepo;
        private IRepository<Consumer> _consumerRepo;

        public AuthService(
            IRepository<Token> tokenRepo,
            IRepository<User> userRepo,
            IRepository<Consumer> consumerRepo
            )
        {
            _tokenRepo = tokenRepo;
            _userRepo = userRepo;
            _consumerRepo = consumerRepo;
        }

        public object GenerateToken(AuthContext context)
        {
            //#01[Auth consumer].Get consumer information. 
            var consumer = _consumerRepo.FirstOrDefault(x => x.Key == context.ConsumerKey && x.Secret == context.ConsumerSecret);
            if (consumer == null)
            {
                //throw new BusinessException("Can not get consumer by 'consumerKey' & 'consumerSecret'!", HttpStatusCode.Unauthorized, ErrorCode.Common_LoginError);
                throw new BusinessException("Can not get consumer by 'consumerKey' & 'consumerSecret'!");
            }

            //#02.Authentication User.
            var factory = AuthFactory.CreateAuthFactory((ConsumerApp)(int.Parse(consumer.Code)));
            var user = _userRepo.FirstOrDefault(x => x.Name == context.UserName && x.Password == context.Password);
            //var user = factory.AuthenticationUser(context.UserName, context.Password);

            //#03.Create or Update token.
            //var key = $"{consumer.Id}-{user.Id}";
            var token = _tokenRepo.FirstOrDefault(x => x.ConsumerId == consumer.Id && x.UserId == user.Id);


            if (token == null)
            {
                //Create token.
                token = Token.GenerateNew(user.Id, consumer.Id);
                //token = Token.GenerateNew(1, consumer.Id);
                _tokenRepo.Insert(token);
            }
            else
            {
                if (token.ExpirationTime < GlobalTime.Now)
                {
                    token.Refresh();
                }
                else
                {
                    token.ExtendExpirationTime();
                }

                _tokenRepo.Update(token);
            }

            //TODO: Cache here.

            return new
            {
                TokenType = "bearer",
                AccessToken = token.AccessToken,
                ExpirationDate = token.ExpirationTime.ToString(),
                RefreshToken = token.RefreshToken,
                User = user
            };
        }
    }
}

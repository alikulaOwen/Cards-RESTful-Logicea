using Libs;
using Models;
using NuGet.Common;
using System.Security.Claims;

namespace Cards.Services.Security
{
    public class SecurityService : SecurityImplService
    {
 
        public UserLoginResModel Login(UserLoginModel model)
        {

            var user = SystemTools.AuthenticateUser(model);
            if (user.Message == ParamsModel.SuccessLogin)
            {
                var token = SystemTools.GenerateToken(user);

                return new UserLoginResModel
                {
                    UserEmail = user.UserEmail,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role,
                    Message = ParamsModel.SuccessLogin,
                    Token = token
                };

            } else
            {
                return new UserLoginResModel
                {
                    Message = ParamsModel.FailLogin
                };
            }

            
        }


    }
}

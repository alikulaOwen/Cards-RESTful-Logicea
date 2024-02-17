using Microsoft.AspNetCore.Identity;
using Models;

namespace Cards.Services.Security
{
    public class SecurityRoute
    {
        SecurityImplService implService = new SecurityService();

        public UserLoginResModel Login(UserLoginModel model)
        {
           return implService.Login(model);
        }
    }
}

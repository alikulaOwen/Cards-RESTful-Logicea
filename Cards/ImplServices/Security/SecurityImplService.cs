using Models;

namespace Cards.Services.Security
{
    public interface SecurityImplService
    {
       public UserLoginResModel Login(UserLoginModel model);
    }
}

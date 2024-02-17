using Dapper;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Libs
{
    public class SystemTools
    {
        public static IDbConnection Connection()
        {
            IDbConnection _db = new SqlConnection(ParamsModel.DBCon.ToString());

            return _db;
        }


        public static string GenerateToken(UserLoginResModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ParamsModel.Key.ToString()));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.UserEmail),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role)
            };


            var token = new JwtSecurityToken(
                ParamsModel.Issuer.ToString(), 
                ParamsModel.Audience.ToString(),
                claims, expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static UserLoginResModel AuthenticateUser(UserLoginModel userLogin)
        {

            var res = Connection().Query<UserLoginResModel>(ParamsModel.SPLogin,

                    new
                    {
                        InputUserEmail = userLogin.UserEmail,
                        InputPassword = userLogin.Password
                    }
                    , null, true, 0, commandType: CommandType.StoredProcedure).FirstOrDefault();


            return res;
        }


    }


}
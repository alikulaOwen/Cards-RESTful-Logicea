using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Cards.Services.Security;

namespace Cards.Controllers.Security
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : Controller
    {
        private readonly SecurityRoute securityRoute = new SecurityRoute();

        private readonly ILogger<SecurityController> logger;

        public SecurityController(ILogger<SecurityController> logger)
        {
            this.logger = logger;
        }


        /// <summary>
        /// This is the first endpoint, use this endpoin to login to the system.
        /// Accespts, user email and password, checks for their validity of and generates a token if the user's email and password are all valid.
        /// The generated token is used to authorize all other endpoints.
        /// </summary>
        /// <returns>
        /// If user's email and password are valied; the endpoint returns:
        /// Username, FirstName, LastName, Role and Token
        /// </returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        [Produces("application/json")]
        public ActionResult<GlobalResponseModel<UserLoginResModel>> Login([FromBody] UserLoginModel userModel)
        {
            
            try
            {
                var loginResponse = new GlobalResponseModel<UserLoginResModel>
                {

                    Status = 200,
                    Message = ParamsModel.RequestSuccessful,
                    Data = securityRoute.Login(userModel)
                };

                
                if (loginResponse.Data.Message == ParamsModel.SuccessLogin)
                {
                    string message = loginResponse.Data.FirstName + " " + ParamsModel.SuccessLogin;
                    logger.LogInformation(message);
                } 
                else
                {
                    string message = loginResponse.Data.FirstName + " " + ParamsModel.FailLogin;
                    logger.LogInformation(message);
                }


                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                var loginResponse = new GlobalResponseModel<UserLoginResModel>
                {

                    Status = 500,
                    Message = ParamsModel.ServerNotResponding,
                    Data = securityRoute.Login(userModel)
                };

                string message = ParamsModel.FailLogin + ": " + ex.Message;
                logger.LogError(message);

                return StatusCode(500, loginResponse);

            }


        }


    }
}

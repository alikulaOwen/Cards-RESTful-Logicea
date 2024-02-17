using Cards.Controllers.Security;
using Cards.Routes.Operatons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Claims;

namespace Cards.Controllers.Operatons
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OperationsController : Controller
    {
        private readonly OperationsRoute operationsRoute = new OperationsRoute();

        private readonly ILogger<SecurityController> logger;

        public OperationsController(ILogger<SecurityController> logger)
        {
            this.logger = logger;
        }




        /// <summary>
        /// CreateCard - Endpoint; used to create and save new cards. In Requestbody, it accepts CardName, CardDescription, and CardColor
        /// If successful, returns cardId, cardName, createdOn, and a message = "Card was successfully"
        /// Authorization is through OAuth 2.0 - Bearer Token
        /// </summary>
        /// <returns>
        /// Status code - 200, if successful and cardId, cardName, createdOn, and a message = "Card was successfully"
        /// </returns>
        [HttpPost("CreateCard")]
        public ActionResult<GlobalResponseModel<CreateCardsResponse>> CreateCard([FromBody] CreateCardsRequest model)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            
            if (identity != null)
            {
                var userClaims = identity.Claims;
                var createdBy = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;


                try
                {
                    var loginResponse = new GlobalResponseModel<CreateCardsResponse>
                    {

                        Status = 200,
                        Message = ParamsModel.RequestSuccessful,
                        Data = operationsRoute.CreateCard(createdBy, model)
                    };


                    if (loginResponse.Data.Message == ParamsModel.SuccessCreated)
                    {
                        string message = loginResponse.Data.CardId + " " + ParamsModel.SuccessCreated;
                        logger.LogInformation(message);
                    }
                    else
                    {
                        logger.LogInformation(ParamsModel.FailCreated);
                    }


                    return Ok(loginResponse);
                }
                catch (Exception ex)
                {
                    var loginResponse = new GlobalResponseModel<UserLoginResModel>
                    {

                        Status = 500,
                        Message = ParamsModel.ServerNotResponding,
                        Data = null
                    };

                    string message = ParamsModel.FailLogin + ": " + ex.Message;
                    logger.LogError(message);

                    return StatusCode(500, loginResponse);

                }


            }
            else
            {
                var loginResponse = new GlobalResponseModel<UserLoginResModel>
                {

                    Status = 401,
                    Message = ParamsModel.ServerNotResponding,
                    Data = null
                };

                string message = ParamsModel.FailLogin + ": " + ParamsModel.NotAuthorized;
                logger.LogError(message);

                return StatusCode(401, loginResponse);
            }

        }



        /// <summary>
        /// GetAllCards - Endpoint; used to retrive all cards from the database. In Requestbody, it accepts PageNumber, PageSize
        /// If successful, returns cardId, cardName, cardDescription, cardColor, createdOn, createdBy, cardStatus
        /// Authorization is through OAuth 2.0 - Bearer Token
        /// </summary>
        /// <returns>
        /// Status code - 200, if successful, returns cardId, cardName, cardDescription, cardColor, createdOn, createdBy, cardStatus
        /// </returns>
        [HttpPost("GetAllCards")]
        public ActionResult<GlobalResponseModel<List<GetCardsResponse>>> GetAllCards([FromBody] GetAllCardsRequest model)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                var createdBy = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;


                try
                {
                    var loginResponse = new GlobalResponseModel<List<GetCardsResponse>>
                    {

                        Status = 200,
                        Message = ParamsModel.RequestSuccessful,
                        Data = operationsRoute.GetAllCards(model, createdBy)
                    };


                    string message = createdBy + " " + ParamsModel.RequestAllCards;
                    logger.LogInformation(message);


                    return Ok(loginResponse);
                }
                catch (Exception ex)
                {
                    var loginResponse = new GlobalResponseModel<GetCardsResponse>
                    {

                        Status = 500,
                        Message = ParamsModel.ServerNotResponding,
                        Data = null
                    };

                    string message = ParamsModel.RequestAllCardsFailed + ": " + ex.Message;
                    logger.LogError(message);

                    return StatusCode(500, loginResponse);

                }


            }
            else
            {
                var loginResponse = new GlobalResponseModel<UserLoginResModel>
                {

                    Status = 401,
                    Message = ParamsModel.ServerNotResponding,
                    Data = null
                };

                string message = ParamsModel.FailLogin + ": " + ParamsModel.NotAuthorized;
                logger.LogError(message);

                return StatusCode(401, loginResponse);
            }
        }






        /// <summary>
        /// GetSpecificCard - Endpoint; used to retrive a specific card from the database, using it's ID. 
        /// In Requestbody, it accepts CardId
        /// If successful, returns cardId, cardName, cardDescription, cardColor, createdOn, createdBy, cardStatus
        /// Authorization is through OAuth 2.0 - Bearer Token
        /// </summary>
        /// <returns>
        /// Status code - 200, if successful, returns cardId, cardName, cardDescription, cardColor, createdOn, createdBy, cardStatus
        /// </returns>
        [HttpPost("GetSpecificCard")]
        public ActionResult<GlobalResponseModel<GetCardsResponse>> GetSpecificCard([FromBody] GetSpecificCardsRequest model)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                var email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;


                try
                {
                    var loginResponse = new GlobalResponseModel<GetCardsResponse>
                    {

                        Status = 200,
                        Message = ParamsModel.RequestSuccessful,
                        Data = operationsRoute.GetSpecificCard(email, model.CardId)
                    };

                    if (loginResponse.Data == null)
                    {
                        loginResponse.Message = ParamsModel.CardDoesNotExist;
                    }

                    string message = email + " " + ParamsModel.RequestedACard;
                    logger.LogInformation(message);


                    return Ok(loginResponse);
                }
                catch (Exception ex)
                {
                    var loginResponse = new GlobalResponseModel<GetCardsResponse>
                    {

                        Status = 500,
                        Message = ParamsModel.ServerNotResponding,
                        Data = null
                    };

                    string message = ParamsModel.RequestACardFailed + ": " + ex.Message;
                    logger.LogError(message);

                    return StatusCode(500, loginResponse);

                }


            }
            else
            {
                var loginResponse = new GlobalResponseModel<UserLoginResModel>
                {

                    Status = 401,
                    Message = ParamsModel.ServerNotResponding,
                    Data = null
                };

                string message = ParamsModel.FailLogin + ": " + ParamsModel.NotAuthorized;
                logger.LogError(message);

                return StatusCode(401, loginResponse);
            }
        }







        /// <summary>
        /// SearchCard - Endpoint; used to search retrive all cards from the database, which contain the searchKey. In Requestbody, it accepts SearchKey, PageNumber, and PageSize
        /// If successful, returns cardId, cardName, cardDescription, cardColor, createdOn, createdBy, cardStatus
        /// Authorization is through OAuth 2.0 - Bearer Token
        /// </summary>
        /// <returns>
        /// Status code - 200, if successful, returns cardId, cardName, cardDescription, cardColor, createdOn, createdBy, cardStatus
        /// </returns>
        [HttpPost("SearchCard")]
        public ActionResult<GlobalResponseModel<List<GetCardsResponse>>> SearchCard([FromBody] SearchCardsRequest model)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                var email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;


                try
                {
                    var loginResponse = new GlobalResponseModel<List<GetCardsResponse>>
                    {

                        Status = 200,
                        Message = ParamsModel.RequestSuccessful,
                        Data = operationsRoute.SearchCard(email, model)
                    };


                    string message = email + " " + ParamsModel.SearchACardFailed;
                    logger.LogInformation(message);


                    return Ok(loginResponse);
                }
                catch (Exception ex)
                {
                    var loginResponse = new GlobalResponseModel<GetCardsResponse>
                    {

                        Status = 500,
                        Message = ParamsModel.ServerNotResponding,
                        Data = null
                    };

                    string message = ParamsModel.RequestACardFailed + ": " + ex.Message;
                    logger.LogError(message);

                    return StatusCode(500, loginResponse);

                }


            }
            else
            {
                var loginResponse = new GlobalResponseModel<UserLoginResModel>
                {

                    Status = 401,
                    Message = ParamsModel.ServerNotResponding,
                    Data = null
                };

                string message = ParamsModel.FailLogin + ": " + ParamsModel.NotAuthorized;
                logger.LogError(message);

                return StatusCode(401, loginResponse);
            }
        }








        /// <summary>
        /// UpdateCard - Endpoint; used to update cards field in the database; these fields are CardName, CardDescription, CardColor, and CardStatus. 
        /// In Requestbody, it accepts CardID, FieldToUpdate which must be either of these ("name", "status", "description", "color" - all in lowercase), and UpdateValue
        /// If successful, returns Message = "Card was updated successfuly"
        /// Authorization is through OAuth 2.0 - Bearer Token
        /// </summary>
        /// <returns>
        /// Status code - 200, if successful, returns Message = "Card was updated successfuly"
        /// </returns>
        [HttpPost("UpdateCard")]
        public ActionResult<GlobalResponseModel<string>> UpdateCard([FromBody] UpdateCard model)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                var email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;


                try
                {
                    var loginResponse = new GlobalResponseModel<string>
                    {

                        Status = 200,
                        Message = ParamsModel.RequestSuccessful,
                        Data = operationsRoute.UpdateCard(model, email)
                    };


                    string message = email + " " + ParamsModel.UpdatedACard;
                    logger.LogInformation(message);


                    return Ok(loginResponse);
                }
                catch (Exception ex)
                {
                    var loginResponse = new GlobalResponseModel<GetCardsResponse>
                    {

                        Status = 500,
                        Message = ParamsModel.ServerNotResponding,
                        Data = null
                    };

                    string message = ParamsModel.UpdatedACardFailed + ": " + ex.Message;
                    logger.LogError(message);

                    return StatusCode(500, loginResponse);

                }


            }
            else
            {
                var loginResponse = new GlobalResponseModel<UserLoginResModel>
                {

                    Status = 401,
                    Message = ParamsModel.ServerNotResponding,
                    Data = null
                };

                string message = ParamsModel.FailLogin + ": " + ParamsModel.NotAuthorized;
                logger.LogError(message);

                return StatusCode(401, loginResponse);
            }
        }












        /// <summary>
        /// DeleteCard - Endpoint; used to delete a specific card from the database, using it's ID
        /// In Requestbody, it accepts CardID
        /// If successful, returns Message = "Card was deleted successfuly"
        /// Authorization is through OAuth 2.0 - Bearer Token
        /// </summary>
        /// <returns>
        /// Status code - 200, if successful, returns Message = "Card was deleted successfuly"
        /// </returns>
        [HttpPost("DeleteCard")]
        public ActionResult<GlobalResponseModel<string>> DeleteCard([FromBody] GetSpecificCardsRequest model)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                var email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;


                try
                {
                    var loginResponse = new GlobalResponseModel<string>
                    {

                        Status = 200,
                        Message = ParamsModel.RequestSuccessful,
                        Data = operationsRoute.DeleteCard(model, email)
                    };


                    string message = email + " " + ParamsModel.DeletedACard;
                    logger.LogInformation(message);


                    return Ok(loginResponse);
                }
                catch (Exception ex)
                {
                    var loginResponse = new GlobalResponseModel<GetCardsResponse>
                    {

                        Status = 500,
                        Message = ParamsModel.ServerNotResponding,
                        Data = null
                    };

                    string message = ParamsModel.DeletedACardFailed + ": " + ex.Message;
                    logger.LogError(message);

                    return StatusCode(500, loginResponse);

                }


            }
            else
            {
                var loginResponse = new GlobalResponseModel<UserLoginResModel>
                {

                    Status = 401,
                    Message = ParamsModel.NotAuthorized,
                    Data = null
                };

                string message = ParamsModel.FailLogin + ": " + ParamsModel.NotAuthorized;
                logger.LogError(message);

                return StatusCode(401, loginResponse);
            }
        }

    }
}

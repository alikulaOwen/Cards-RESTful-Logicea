using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ParamsModel
    {
        public static string DBCon { get; set; }
        public static string Key { get; set; }
        public static string Issuer { get; set; }
        public static string Audience { get; set; }
        public static string SuccessLogin { get; set; }
        public static string SuccessCreated { get; set; }
        public static string FailCreated { get; set; }
        public static string FailLogin { get; set; }
        public static string ApiDocVersion { get; set; }
        public static string RequestSuccessful { get; set; }
        public static string ServerNotResponding { get; set; }
        
        public static string NotAuthorized { get; set; }
        public static string RequestAllCards { get; set; }
        public static string RequestAllCardsFailed { get; set; }
        public static string RequestedACard { get; set; }
        
        public static string RequestACardFailed { get; set; }
        public static string SearchACardFailed { get; set; }
        public static string UpdatedACard { get; set; }
        public static string UpdatedACardFailed { get; set; }
        public static string DeletedACard { get; set; }
        public static string DeletedACardFailed { get; set; }
        public static string NotValid { get; set; }
        public static string ColorError { get; set; }
        public static string EmptyString { get; set; }

        public static string HushSymbol { get; set; }
        public static string StringName { get; set; }
        public static string StringStatus { get; set; }
        public static string NameStatusCanNotBeEmpty { get; set; }
        public static string DefinedRegex { get; set; }
        public static string CardDoesNotExist { get; set; }
        


        public static string SPLogin { get; set; }
        public static string SPAddCard { get; set; }
        public static string SPGetAllCards { get; set; }
        public static string SPGetSpecificCards { get; set; }
        public static string SPSearchForCards { get; set; }
        public static string SPUpdateSpecificCards { get; set; }
        public static string SPDeleteSpecificCards { get; set; }
    }

    public class TokenResultModel
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string TokenID { get; set; }
        public string Password { get; set; }
        public string MacAddress { get; set; }
        public string DeviceID { get; set; }
        public string IpAddress { get; set; }
        public bool IsActive { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
    }

    public class IdentityModel
    {
        public string TokenID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string WorkingDate { get; set; }
    }


    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
    }


    public class UserLoginModel
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }
    }


    public class UserLoginResModel
    {
        public string UserEmail { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }


    public class GlobalResponseModel<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code indicating the success or failure of the operation.
        /// </summary>
        public T Data { get; set; }

    }


    public class CreateCardsRequest
    {
        [NonEmptyString(ErrorMessage = "CardName must be a non-empty string")]
        public string CardName { get; set; }

        public string CardDescription { get; set; }
        public string CardColor { get; set; }

    }





    public class CreateCardsResponse
    {
        public string CardId { get; set; }
        public string CardName { get; set; }
        public string Message { get; set; }

        public DateTime CreatedOn { get; set; }

    }


    public class GetCardsResponse
    {
        public string CardId { get; set; }
        public string CardName { get; set; }
        public string CardDescription { get; set; }
        public string CardColor { get; set; }
        public string CreatedBy { get; set; }
        public string CardStatus { get; set; }
        public DateTime CreatedOn { get; set; }

    }


    public class GetAllCardsRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }


    public class GetSpecificCardsRequest
    {
        public string CardId { get; set; }

    }


    public class SearchCardsRequest
    {
        public string SearchKey { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }


    public class UpdateCard
    {
        public string CardID { get; set; }
        public string FieldToUpdate { get; set; }
        public string UpdateValue { get; set; }
    }



    public class NonEmptyStringAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var getVal = value;
            if (value == null)
                return false;

            string stringValue = value.ToString();
            return !string.IsNullOrWhiteSpace(stringValue);
        }
    }


}
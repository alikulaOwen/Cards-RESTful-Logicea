using Cards.ImplServices.Operatons;
using Dapper;
using Libs;
using Models;
using System.Data;
using System.Text.RegularExpressions;

namespace Cards.Services.Operatons
{
    public class OperationsService : OperationsImplService
    {

        IDbConnection dbConnection = SystemTools.Connection();
        public CreateCardsResponse CreateCard(string cretedBy, CreateCardsRequest model)
        {
            var colorString = CheckValidityOfColor(model.CardColor);

            if (colorString == ParamsModel.NotValid)
            {
                var res = new CreateCardsResponse()
                {
                    CardId = null,
                    CardName = null,
                    Message = ParamsModel.ColorError

                };
                return res;
            }
            else
            {

                var res = dbConnection.Query<CreateCardsResponse>(ParamsModel.SPAddCard,

                    new
                    {
                        model.CardName,
                        model.CardDescription,
                        CardColor = colorString,
                        CreateBy = cretedBy
                    }
                    , null, true, 0, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return res;
            }




        }


        public string CheckValidityOfColor(string color)
        {
            if (color != ParamsModel.EmptyString)
            {
                if (color.Length != 6 || ContainsSpecialCharacter(color))
                {
                    return ParamsModel.NotValid;
                }
                else
                {
                    return ParamsModel.HushSymbol + color;
                }
            }
            else
            {
                return color;
            }

        }


        static bool ContainsSpecialCharacter(string input)
        {
            Regex regex = new Regex(ParamsModel.DefinedRegex);

            return regex.IsMatch(input);
        }
        


        public List<GetCardsResponse> GetAllCards(GetAllCardsRequest model, string userEmail)
        {
            var res = dbConnection.Query<GetCardsResponse>(ParamsModel.SPGetAllCards,

                    new
                    {
                        userEmail,
                        model.PageNumber,
                        model.PageSize
                    }
                    , null, true, 0, commandType: CommandType.StoredProcedure).AsList();


            return res;


        }



        public GetCardsResponse GetSpecificCard(string userEmail, string cardID)
        {
            var res = dbConnection.Query<GetCardsResponse>(ParamsModel.SPGetSpecificCards,

                    new
                    {
                        userEmail,
                        cardID
                    }
                    , null, true, 0, commandType: CommandType.StoredProcedure).FirstOrDefault();

            return res;

        }



        public List<GetCardsResponse> SearchCard(string userEmail, SearchCardsRequest model)
        {
            var res = dbConnection.Query<GetCardsResponse>(ParamsModel.SPSearchForCards,

                    new
                    {
                        userEmail,
                        model.SearchKey,
                        model.PageNumber,
                        model.PageSize
                    }
                    , null, true, 0, commandType: CommandType.StoredProcedure).AsList();


            return res;

        }



        public string UpdateCard(UpdateCard model, string userEmail)
        {
            var res = ParamsModel.EmptyString;

            if (model.FieldToUpdate == ParamsModel.StringName || model.FieldToUpdate == ParamsModel.StringStatus)
            {
                if (model.UpdateValue == ParamsModel.EmptyString)
                {
                    res = ParamsModel.NameStatusCanNotBeEmpty;
                }
                else
                {
                    res = dbConnection.Query<string>(ParamsModel.SPUpdateSpecificCards,

                    new
                    {
                        userEmail,
                        model.CardID,
                        model.FieldToUpdate,
                        model.UpdateValue
                    }
                    , null, true, 0, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            else
            {
                res = dbConnection.Query<string>(ParamsModel.SPUpdateSpecificCards,

                new
                {
                    userEmail,
                    model.CardID,
                    model.FieldToUpdate,
                    model.UpdateValue
                }
                , null, true, 0, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }



            return res;
        }



        public string DeleteCard(GetSpecificCardsRequest model, string userEmail)
        {
            var res = dbConnection.Query<string>(ParamsModel.SPDeleteSpecificCards,

                    new
                    {
                        userEmail,
                        model.CardId
                    }
                    , null, true, 0, commandType: CommandType.StoredProcedure).FirstOrDefault();


            return res;

        }

    }
}

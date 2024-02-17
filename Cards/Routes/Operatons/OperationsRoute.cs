using Cards.ImplServices.Operatons;
using Cards.Services.Operatons;
using Cards.Services.Security;
using Models;

namespace Cards.Routes.Operatons
{
    public class OperationsRoute
    {
        OperationsImplService implService = new OperationsService();

        public CreateCardsResponse CreateCard(string cretedBy, CreateCardsRequest model)
        {
            return implService.CreateCard(cretedBy, model);
        }



        public List<GetCardsResponse> GetAllCards(GetAllCardsRequest model, string email)
        {
            return implService.GetAllCards(model, email);
        }



        public GetCardsResponse GetSpecificCard(string email, string cardID)
        {
            return implService.GetSpecificCard(email, cardID);
        }



        public List<GetCardsResponse> SearchCard(string email, SearchCardsRequest model)
        {
            return implService.SearchCard(email, model);
        }



        public string UpdateCard(UpdateCard model, string email)
        {
            return implService.UpdateCard(model, email);
        }



        public string DeleteCard(GetSpecificCardsRequest model, string email)
        {
            return implService.DeleteCard(model, email);
        }
    }
}

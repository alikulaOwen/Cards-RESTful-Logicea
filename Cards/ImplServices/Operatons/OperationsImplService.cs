using Models;

namespace Cards.ImplServices.Operatons
{
    public interface OperationsImplService
    {
        public CreateCardsResponse CreateCard(string cretedBy, CreateCardsRequest model);

        public List<GetCardsResponse> GetAllCards(GetAllCardsRequest model, string email);

        public GetCardsResponse GetSpecificCard(string email, string cardID);

        public List<GetCardsResponse> SearchCard(string email, SearchCardsRequest model);

        public string UpdateCard(UpdateCard model, string email);

        public string DeleteCard(GetSpecificCardsRequest model, string email);
    }
}

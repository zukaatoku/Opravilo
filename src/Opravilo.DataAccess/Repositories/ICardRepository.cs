using Opravilo.DataAccess.Dto;

namespace Opravilo.DataAccess.Repositories
{
    public interface ICardRepository
    {
        CardDto CreateCard(long stateId, string name, string description);
        CardDto UpdateCard(long cardId, string name, string description);
        void RemoveCard(long cardId);
    }
}
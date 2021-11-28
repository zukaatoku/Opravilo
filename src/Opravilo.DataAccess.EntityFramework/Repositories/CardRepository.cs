using System;
using System.Linq;
using Opravilo.DataAccess.Dto;
using Opravilo.DataAccess.EntityFramework.Models;
using Opravilo.DataAccess.Repositories;

namespace Opravilo.DataAccess.EntityFramework.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly DataContext _context;

        public CardRepository(DataContext context)
        {
            _context = context;
        }

        public CardDto CreateCard(long stateId, string name, string description)
        {
            var now = DateTime.Now;
            var card = new CardModel()
            {
                Name = name,
                Description = description,
                StateId = stateId,
                ChangedDate = now,
                CreatedDate = now
            };

            _context.Cards.Add(card);
            _context.SaveChanges();

            return new CardDto()
            {
                Id = card.Id,
                Name = card.Name,
                Description = card.Description
            };
        }

        public CardDto UpdateCard(long cardId, string name, string description)
        {
            var now = DateTime.Now;
            var card = _context.Cards.First(c => c.Id == cardId);

            card.Name = name;
            card.Description = description;
            card.ChangedDate = now;

            _context.Update(card);
            _context.SaveChanges();

            return new CardDto()
            {
                Id = card.Id,
                Name = card.Name,
                Description = card.Description
            };
        }

        public void ChangeState(long cardId, long newStateId)
        {
            var now = DateTime.Now;
            var card = _context.Cards.First(c => c.Id == cardId);

            card.StateId = newStateId;
            card.ChangedDate = now;

            _context.Update(card);
            _context.SaveChanges();
        }

        public void RemoveCard(long cardId)
        {
            var card = _context.Cards.First(c => c.Id == cardId);
            _context.Cards.Remove(card);
            _context.SaveChanges();
        }
    }
}
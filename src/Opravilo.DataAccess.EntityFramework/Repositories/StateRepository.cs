using System;
using System.Collections.Generic;
using System.Linq;
using Opravilo.DataAccess.Dto;
using Opravilo.DataAccess.EntityFramework.Models;
using Opravilo.DataAccess.Repositories;

namespace Opravilo.DataAccess.EntityFramework.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly DataContext _context;

        public StateRepository(DataContext context)
        {
            _context = context;
        }

        public List<StateDto> CreateStates(long projectId, List<string> names)
        {
            var now = DateTime.Now;

            var statesToAdd = names.Select(n => new StateModel()
            {
                Name = n,
                ProjectId = projectId,
                ChangedDate = now,
                CreatedDate = now
            }).ToList();

            _context.States.AddRange(statesToAdd);
            _context.SaveChanges();

            return statesToAdd.Select(s => new StateDto()
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }

        public StateDto CreateState(long projectId, string name)
        {
            var now = DateTime.Now;
            var newState = new StateModel()
            {
                Name = name,
                ProjectId = projectId,
                ChangedDate = now,
                CreatedDate = now
            };

            _context.States.Add(newState);
            _context.SaveChanges();
            return new StateDto()
            {
                Id = newState.Id,
                Name = newState.Name
            };
        }

        public StateDto UpdateState(long stateId, long projectId, string name)
        {
            var now = DateTime.Now;
            var state = _context.States.First(s => s.Id == stateId);
            state.Name = name;
            state.ChangedDate = now;
            _context.Update(state);
            _context.SaveChanges();
            return new StateDto()
            {
                Id = state.Id,
                Name = state.Name
            };
        }

        public void RemoveState(long stateId)
        {
            var state = _context.States.First(s => s.Id == stateId);
            _context.States.Remove(state);
            _context.SaveChanges();
        }
    }
}
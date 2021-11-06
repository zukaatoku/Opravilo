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
    }
}
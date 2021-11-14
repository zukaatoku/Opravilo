using System.Collections.Generic;
using Opravilo.DataAccess.Dto;

namespace Opravilo.DataAccess.Repositories
{
    public interface IStateRepository
    {
        List<StateDto> CreateStates(long projectId, List<string> names);
        StateDto CreateState(long projectId, string name);
        StateDto UpdateState(long stateId, long projectId, string name);
        void RemoveState(long stateId);
    }
}
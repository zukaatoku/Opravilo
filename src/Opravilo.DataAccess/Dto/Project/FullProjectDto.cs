using System.Collections.Generic;

namespace Opravilo.DataAccess.Dto.Project
{
    public class FullProjectDto
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public UserDto Creator { get; init; }
        public List<StateDto> States { get; init; }
    }
}
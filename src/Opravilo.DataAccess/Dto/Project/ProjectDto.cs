using System.Collections.Generic;

namespace Opravilo.DataAccess.Dto.Project
{
    public record ProjectDto
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public UserDto Creator { get; init; }
    }
}
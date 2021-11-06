using System.Collections.Generic;
using Opravilo.Application.Models.User;

namespace Opravilo.Application.Models.Project
{
    public class FullProjectModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public UserModel Creator { get; set; }
        public List<StateModel> States { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opravilo.DataAccess.EntityFramework.Models
{
    public class ProjectPermissionModel
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        
        [Column("NAME")]
        public string Name { get; set; }
    }
}
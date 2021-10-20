using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Opravilo.DataAccess.EntityFramework.Models
{
    public class ProjectUserModel
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        
        [Column("PROJECT_ID")]
        public long ProjectId { get; set; }
        
        [Column("USER_ID")]
        public long UserId { get; set; }
        
        [Column("PERMISSION_ID")]
        public long PermissionId { get; set; }
        
        [Column("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }
        
        [Column("CHANGED_DATE")]
        public DateTime ChangedDate { get; set; }
        
        [ForeignKey(nameof(ProjectId))]
        public virtual ProjectModel Project { get; set; }
    }
}
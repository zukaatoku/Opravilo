using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opravilo.DataAccess.EntityFramework.Models
{
    public class ProjectModel
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        
        [Column("NAME")]
        public string Name { get; set; }
        
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        
        [Column("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }
        
        [Column("CHANGED_DATE")]
        public DateTime ChangedDate { get; set; }
        
        public virtual List<ProjectUserModel> UsersPermissions { get; set; } 
    }
}
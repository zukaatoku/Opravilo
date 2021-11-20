using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opravilo.DataAccess.EntityFramework.Models
{
    public class StateModel
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        
        [Column("NAME")]
        public string Name { get; set; }
        
        [Column("PROJECT_ID")]
        public long ProjectId { get; set; }
        
        [ForeignKey(nameof(ProjectId))]
        public virtual ProjectModel Project { get; set; }
        
        [Column("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }
        
        [Column("CHANGED_DATE")]
        public DateTime ChangedDate { get; set; }
        
        public virtual List<CardModel> Cards { get; set; }
    }
}
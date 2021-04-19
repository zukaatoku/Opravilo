using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opravilo.DataAccess.EntityFramework.Models
{
    public class UserModel
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        
        [Column("LOGIN")]
        public string Login { get; set; }
        
        [Column("PASSWORD_HASH")]
        public string PasswordHash { get; set; }
        
        [Column("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }
        
        [Column("CHANGED_DATE")]
        public DateTime ChangedDate { get; set; }
    }
}
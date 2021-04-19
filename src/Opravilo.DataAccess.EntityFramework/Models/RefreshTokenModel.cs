using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opravilo.DataAccess.EntityFramework.Models
{
    public class RefreshTokenModel
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        
        [Column("USER_ID")]
        public long UserId { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public virtual UserModel User { get; set; }
        
        [Column("REFRESH_TOKEN")]
        public string RefreshToken { get; set; }
        
        [Column("EXPIRATION_DATE")]
        public DateTime ExpirationDate { get; set; }
        
        [Column("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }
        
        [Column("CHANGED_DATE")]
        public DateTime ChangedDate { get; set; }
    }
}
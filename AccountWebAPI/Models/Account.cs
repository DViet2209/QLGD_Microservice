using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountWebAPI.Models
{
    [Table("account", Schema = "dbo")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("account_id")]
        public int AccountId { get; set; }
        [Column("account_userid")]
        public string AccountUserId { get; set; }
        [Column("account_surname")]
        public string AccountSurName { get; set; }
        [Column("account_name")]
        public string AccountUserName { get; set; }
        [Column("account_password")]
        public string AccountPassword { get; set; }
        [Column("account_email")]
        public string AccountParents { get; set; }
        [Column("account_parents")]
        public string AccountEmail { get; set; }
        [Column("account_sex")]
        public string AccountSex { get; set; }
        [Column("account_address")]
        public string AccountAddress { get; set; }
        [Column("account_birthdate")]
        public DateTime AccountBirthDate { get; set; }
        [Column("account_phone")]
        public string AccountPhone { get; set; }
        [Column("account_avatar")]
        public string AccountAvatar { get; set; }
    }
}

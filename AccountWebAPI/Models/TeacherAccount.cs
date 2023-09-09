using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountWebAPI.Models
{
    [Table("teacheraccount", Schema = "dbo")]
    public class TeacherAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("teacheraccount_id")]
        public int TeacherAccountId { get; set; }
        [Column("teacheraccount_userid")]
        public string TeacherAccountUserId { get; set; }
        [Column("teacheraccount_surname")]
        public string TeacherAccountSurName { get; set; }
        [Column("teacheraccount_name")]
        public string TeacherAccountUserName { get; set; }
        [Column("teacheraccount_password")]
        public string TeacherAccountPassword { get; set; }
        [Column("teacheraccount_email")]
        public string TeacherAccountEmail { get; set; }
        [Column("teacheraccount_sex")]
        public string TeacherAccountSex { get; set; }
        [Column("teacheraccount_address")]
        public string TeacherAccountAddress { get; set; }
        [Column("teacheraccount_birthdate")]
        public DateTime TeacherAccountBirthDate { get; set; }
        [Column("teacheraccount_phone")]
        public string TeacherAccountPhone { get; set; }
        [Column("teacheraccount_avatar")]
        public string TeacherAccountAvatar { get; set; }
        [Column("teacheraccount_mainteachingsubject")]
        public string TeacherAccountMainTeachingSubject { get; set; }
        [Column("teacheraccount_concurrentsubjects")]
        public string TeacherAccountConcurrentSubject { get; set; }
        [Column("teacheraccount_taxcode")]
        public string TeacherAccountTaxCode { get; set; }
    }
}

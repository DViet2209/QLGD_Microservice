using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWebAPI.Models
{
    [Table("tuition", Schema = "dbo")]
    public class Tuition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("tuition_id")]
        public int TuitionId { get; set; } // khóa chính

        [Column("tuition_Account_userid")]
        public string? AccountUserId { get; set; }

        [Column("tuition_courseid")]
        public int CourseId { get; set; }

        [Column("tuition_status")]
        public string? TuitionStatus { get; set; }

        [Column("tuition_datetime")]
        public DateTime TuitionDateTime { get; set; }

        [Column("tuition_promotion")]
        public int TuitionPromotion { get; set; }
    }
}

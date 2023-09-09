using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWebAPI.Models
{
    [Table("course", Schema = "dbo")]
    public class Course
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("course_id")]
        public int CourseId { get; set; } // khóa chính

        [Column("course_name")]
        public string CourseName { get; set; }

        [Column("course_openingday")]
        public DateTime CourseOpeningDay { get; set; }

        [Column("course_describe")]
        public string CourseDescribe { get; set; }

        [Column("course_numberofparticipants")]
        public int CourseNumberOfParticipants { get; set; }

        [Column("course_tuition")]
        public int CourseTuition { get; set; }

        [Column("category_Id")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

    }

}

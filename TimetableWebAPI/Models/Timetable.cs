using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimetableWebAPI.Models
{
    [Table("Timetable", Schema = "dbo")]
    public class TimeTable
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("timetable_id")]
        public int TimetableId { get; set; }
        [Column("timetable_idaccount")]
        public int TimetableIdAccount { get; set; }
        [Column("timetable_courseid")]
        public int TimetableCourseId { get; set; }
        [Column("timetable_begintime")]
        public DateTime TimetableBeginTime { get; set; }
        [Column("timetable_endtime")]
        public DateTime TimetableEndTime { get; set; }
        [Column("timetable_status")]
        public string TimetableStatus { get; set; }

    }
}

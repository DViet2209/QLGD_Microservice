using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseWebAPI.Models
{

    [Table("category", Schema = "dbo")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("category_id")]
        public int CategoryId { get; set; } // khóa chính
        [Column("category_name")]
        public string CategoryName { get; set; }
    }

}

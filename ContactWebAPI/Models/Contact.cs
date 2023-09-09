using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactWebAPI.Models
{
    [Table("contact", Schema = "dbo")]
    public class Contact
    {

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Column("contact_id")]
            public int ContactId { get; set; }
            [Column("contact_name")]
            public string ContactName { get; set; }
            [Column("contact_phone")]
            public string ContactPhone { get; set; }
            [Column("contact_email")]
            public string ContactEmail { get; set; }
            [Column("contact_messenger")]
            public string ContactMessenger { get; set; }
            

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAF.Infrastructure.Data.Models
{
    public class Emails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string SenderEmail { get; set; }

        [Required]
        [StringLength(500,MinimumLength =50)]
        public string Message { get; set; }
    }
}
    
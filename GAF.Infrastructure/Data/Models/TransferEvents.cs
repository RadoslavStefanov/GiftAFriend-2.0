using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAF.Infrastructure.Data.Models
{
    public class TransferEvents
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        public string SenderName { get; set; }

        [Required]
        public string RecieverId { get; set; }

        [Required]
        [ForeignKey(nameof(RecieverId))]
        public IdentityUser Reciever { get; set; }

        [Required]
        public string RecieverName { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        [Range(minimum: 0, maximum: double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
    }
}

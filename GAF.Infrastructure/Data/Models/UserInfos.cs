
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAF.Infrastructure.Data.Models
{
    public class UserInfos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

        [StringLength(20, MinimumLength = 6)]
        public string MobileNumber { get; set; }

        [Required]
        [Range(minimum: 0, maximum: double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal GiftTokens { get; set; }


        public string Address { get; set; }
    }
}

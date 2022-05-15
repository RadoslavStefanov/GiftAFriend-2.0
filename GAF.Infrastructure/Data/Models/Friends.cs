using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAF.Infrastructure.Data.Models
{
    public class Friends
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }


        [Required]
        public string FriendId { get; set; }

        [Required]
        [ForeignKey(nameof(FriendId))]
        public IdentityUser Friend { get; set; }
    }
}

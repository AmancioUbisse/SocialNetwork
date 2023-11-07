using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Models.Domain
{
    public class Staff
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        public int IsActive { get; set; }
    }
}

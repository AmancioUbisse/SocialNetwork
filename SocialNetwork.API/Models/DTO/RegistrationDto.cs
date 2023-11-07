using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Models.DTO
{
    public class RegistrationDto
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

        [Required]
        [StringLength(50)]
        public string PhoneNo { get; set; }
        public int IsActive { get; set; }
        public int IsApproved { get; set; }
    }
}

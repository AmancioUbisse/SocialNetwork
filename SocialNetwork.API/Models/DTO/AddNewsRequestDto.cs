using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Models.DTO
{
    public class AddNewsRequestDto
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Content { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Image { get; set; }
        public int IsActive { get; set; }
        public int IsApproved { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

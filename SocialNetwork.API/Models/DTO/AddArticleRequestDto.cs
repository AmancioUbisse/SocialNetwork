using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Models.DTO
{
    public class AddArticleRequestDto
    {

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Content { get; set; }

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

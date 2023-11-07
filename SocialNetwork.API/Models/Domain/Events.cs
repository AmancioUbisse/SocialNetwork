using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.API.Models.Domain
{
    [Table("Events")]
    public class Events
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Content { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public int IsActive { get; set; }
        public DateTime CreatedOn { get; set; }

      
    }

}

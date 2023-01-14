using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Disciples2ClientDataBaseModels.DBModels
{
    [Table("Mods")]
    public class Mod
    {
        [Key]
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTimeOffset FirstUpdateDateTime { get; set; }
        [Required]
        public DateTimeOffset LastUpdateDateTime { get; set; }
        [Required]
        public string Version { get; set; }
        [ForeignKey("ModAuthor")]
        public int AuthorUserId { get; set; }
        public User Author { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public bool IsSoftware { get; set; } = false;
    }
}

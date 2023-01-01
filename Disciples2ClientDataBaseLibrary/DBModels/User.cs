using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Disciples2ClientDataBaseModels.DBModels
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ConcurrencyCheck]
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [ConcurrencyCheck]
        public string? Email { get; set; }
        [Required]
        public string[] Roles { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        public List<Mod> Mods { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Disciples2ApiModels.ApiModels
{
    public class CreateUserModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        [Required]
        public string[] Roles { get; set; }
    }
}

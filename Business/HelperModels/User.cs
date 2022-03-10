using System.ComponentModel.DataAnnotations;

namespace Business.HelperModels
{
    public class User
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

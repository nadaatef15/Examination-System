using System.ComponentModel.DataAnnotations;

namespace Exam_System.Models
{
    public class UserLoginView
    {
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace NguyenHoangKhang_Test_3.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public IFormFile Avatar { get; set; }
    }
}

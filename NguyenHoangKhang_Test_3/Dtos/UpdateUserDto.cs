using System.ComponentModel.DataAnnotations;

namespace NguyenHoangKhang_Test_3.Dtos
{
    public class UpdateUserDto
    {
        [Required]
        public int Id { get; set; }
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
        public IFormFile Avatar { get; set; } = null;
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenHoangKhang_Test_3.Data
{
    [Table("user")]
    [Index(nameof(Email), nameof(Phone))]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [Required]
        public string Avatar { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

    }
}

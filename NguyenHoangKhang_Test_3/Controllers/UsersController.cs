using Microsoft.AspNetCore.Mvc;
using NguyenHoangKhang_Test_3.Data;
using NguyenHoangKhang_Test_3.Dtos;
using NguyenHoangKhang_Test_3.Repository;

namespace NguyenHoangKhang_Test_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(ILogger<UsersController> logger, IUserRepository userRepository, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("getuserbyid")]
        public async Task<IActionResult> GetUserByID(int id)
        {
            try
            {
                User user = await _userRepository.Get(x => x.Id == id);
                if (user == null)
                {
                    return NotFound("User Not Found");
                }
                user.Avatar = GetFile(user.Avatar);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
        [HttpGet("getalluser")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                List<User> users = await _userRepository.GetAll(x => true);
                foreach (var user in users)
                {
                    user.Avatar = GetFile(user.Avatar);
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserDto userDto)
        {
            try
            {
                User userExists = await _userRepository.Get(x => x.Email == userDto.Email || x.Phone == userDto.Email);
                if (userExists != null)
                {
                    return BadRequest("Email or phone already exists");
                }
                var user = new User
                {
                    Email = userDto.Email,
                    Phone = userDto.Phone,
                    Birthday = userDto.Birthday,
                    CreatedAt = DateTime.Now,
                    Gender = userDto.Gender,
                    Name = userDto.Name,
                    Password = userDto.Password
                };
                user.Avatar = await UploadFileAsync(userDto.Avatar);
                var result = await _userRepository.Create(user);
                if (result)
                {
                    return Ok("Create User Success");
                }
                throw new Exception("Create User Error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
        [HttpPut("updateuser")]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserDto updateUserDto)
        {
            try
            {
                User user = await _userRepository.Get(x => x.Id == updateUserDto.Id);
                if (user == null)
                {
                    return NotFound("User Not Found");
                }
                user.Email = updateUserDto.Email;
                user.Phone = updateUserDto.Phone;
                user.Birthday = updateUserDto.Birthday;
                user.Gender = updateUserDto.Gender;
                user.Name = updateUserDto.Name;
                user.Password = updateUserDto.Password;


                if (updateUserDto.Avatar != null)
                {
                    var oldUrl = user.Avatar;
                    user.Avatar = await UploadFileAsync(updateUserDto.Avatar);
                }
                var result = await _userRepository.Update(user);
                if (result)
                {
                    return Ok("Update User Success");
                }
                throw new Exception("Update User Error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
        [HttpDelete("deleteuser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                User user = await _userRepository.Get(x => x.Id == id);
                if (user == null)
                {
                    return NotFound("User Not Found");
                }
                var result = await _userRepository.Delete(user);
                if (result)
                {
                    return Ok("Delete successfull");
                }
                throw new Exception("Delete error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                User user = await _userRepository.Get(x => x.Email == loginDto.username || x.Phone == loginDto.username);
                if (user == null)
                {
                    return BadRequest("Phone, email or password incorect");
                }
                if (user.Password == loginDto.password)
                {
                    // Chổ này sẽ trả về token hoặc ...
                    // Do đề không yêu cầu cái gì nên em return message success
                    return Ok("Login Successfull");
                }
                return BadRequest("Phone, email or password incorect");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
        [HttpDelete("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPassword)
        {
            try
            {
                User user = await _userRepository.Get(x => x.Email == forgotPassword.username || x.Phone == forgotPassword.username);
                if (user == null)
                {
                    return BadRequest("User not found");
                }
                // nếu email hoặc phone đúng thì sửa lý gửi mail hoặc gửi message qua phone,....
                if (forgotPassword.newPassword == forgotPassword.confirmPassword)
                {
                    user.Password = forgotPassword.newPassword;
                    var result = await _userRepository.Update(user);
                    if (result)
                    {
                        return Ok("Change password successfull");
                    }
                    throw new Exception("Update user error");
                }
                return BadRequest("Password and confirmPassword not same");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
        private async Task<string> UploadFileAsync(IFormFile File)
        {
            try
            {

                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, File.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await File.CopyToAsync(stream);
                }
                return File.FileName;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        private string GetFile(string Key)
        {
            string scheme = "https";
            var host = _httpContextAccessor.HttpContext.Request.Host.Value;
            Uri uri = new Uri($"{scheme}://{host}/{Key}");
            return uri.ToString();
        }
    }
}

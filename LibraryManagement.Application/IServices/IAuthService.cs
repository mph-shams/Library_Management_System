using Library_Management_System.LibraryManagement.Application.DTOs;

namespace LibraryManagement.Application.IServices
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
        Task<UserDto> RegisterAsync(RegisterRequestDto request);
        Task<UserDto?> GetCurrentUserAsync(int userId);
        Task<UserDto?> GetUserByIdAsync(int userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}
using Library_Management_System.LibraryManagement.Application.DTOs;
using Library_Management_System.LibraryManagement.Core.Entities;
using Library_Management_System.LibraryManagement.Core.Enums;
using LibraryManagement.Application.IRepositories;
using LibraryManagement.Application.IServices;
using LibraryManagement.Infrastructure.Interfaces;
using LibraryManagementApi.Exceptions;
using System.Data;

namespace Library_Management_System.LibraryManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new ValidationException("نام کاربری یا رمز عبور اشتباه است");
            }

            var token = _tokenService.GenerateToken(user);
            var expiresAt = DateTime.UtcNow.AddHours(24);

            return new LoginResponseDto
            {
                Token = token,
                ExpiresAt = expiresAt,
                User = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = user.Role.ToString(),
                    CreatedAt = user.CreatedAt
                }
            };
        }

        public async Task<UserDto> RegisterAsync(RegisterRequestDto request)
        {
            if (await _unitOfWork.Users.ExistsByUsernameAsync(request.Username))
            {
                throw new ConflictException("این نام کاربری قبلاً استفاده شده است");
            }

            if (await _unitOfWork.Users.ExistsByEmailAsync(request.Email))
            {
                throw new ConflictException("این ایمیل قبلاً استفاده شده است");
            }

            if (!Enum.TryParse<Role>(request.Role, ignoreCase: true, out var roleEnum))
            {
                roleEnum = Role.Member; 
            }

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FullName = request.FullName,
                Role = roleEnum,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role.ToString(),
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserDto?> GetCurrentUserAsync(int userId)
        {
            return await GetUserByIdAsync(userId);
        }

        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);

            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role.ToString(),
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                FullName = u.FullName,
                Role = u.Role.ToString(),
                CreatedAt = u.CreatedAt
            });
        }
    }
}

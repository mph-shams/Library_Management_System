using Library_Management_System.LibraryManagement.Core.Entities;

namespace LibraryManagement.Application.IServices
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
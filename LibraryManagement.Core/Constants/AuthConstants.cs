namespace Library_Management_System.LibraryManagement.Core.Constants
{
    public static class AuthConstants
    {
        public const int MinPasswordLength = 6;
        public const int MaxPasswordLength = 100;
        public const int MinUsernameLength = 3;
        public const int MaxUsernameLength = 50;
        public const int TokenExpirationMinutes = 60;
        public const string AdminRole = "Admin";
        public const string LibrarianRole = "Librarian";
        public const string MemberRole = "Member";
    }
}

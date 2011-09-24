namespace LearnMVC.Services
{
    public interface IAuthorizationService
    {
        bool SignIn(string userName, string password, bool rememberMe);
        void SignOut();

        UserCreationResult CreateUser(string userName, string rawPassword, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);

        bool IsUserExists(string userName);

        int MinPasswordLength { get; }
    }
}
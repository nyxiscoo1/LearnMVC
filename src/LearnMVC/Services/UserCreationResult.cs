namespace LearnMVC.Services
{
    public class UserCreationResult
    {
        public bool IsOk { get; private set; }
        public string ErrorMessage { get; private set; }

        public UserCreationResult(bool isOk, string errorMessage)
        {
            IsOk = isOk;
            ErrorMessage = errorMessage;
        }
    }
}
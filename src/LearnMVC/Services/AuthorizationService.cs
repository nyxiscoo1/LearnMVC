using System;
using System.Web.Security;

namespace LearnMVC.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public bool SignIn(string userName, string rawPassword, bool rememberMe)
        {
            bool isValid = Membership.ValidateUser(userName, rawPassword.GetMD5Hash());
            if (isValid)
                FormsAuthentication.SetAuthCookie(userName, rememberMe);

            return isValid;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public bool ChangePassword(string userName, string rawOldPassword, string rawNewPassword)
        {
            bool changePasswordSucceeded;
            try
            {
                MembershipUser currentUser = Membership.GetUser(userName, true /* userIsOnline */);
                changePasswordSucceeded = currentUser.ChangePassword(rawOldPassword.GetMD5Hash(), rawNewPassword.GetMD5Hash());
            }
            catch (Exception)
            {
                changePasswordSucceeded = false;
            }
            return changePasswordSucceeded;
        }

        public bool IsUserExists(string userName)
        {
            return Membership.GetUser(userName, false) != null;
        }

        public int MinPasswordLength
        {
            get { return Membership.MinRequiredPasswordLength; }
        }

        public UserCreationResult CreateUser(string userName, string rawPassword, string email)
        {
            MembershipCreateStatus createStatus;
            Membership.CreateUser(userName, rawPassword.GetMD5Hash(), email, null, null, true, null, out createStatus);

            bool isOk = createStatus == MembershipCreateStatus.Success;
            string errorMessage = ErrorCodeToString(createStatus);

            return new UserCreationResult(isOk, errorMessage);
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.Success:
                    return string.Empty;

                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }


        }
    }
}
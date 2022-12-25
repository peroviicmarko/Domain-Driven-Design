namespace DDD.Application.Common
{
    public static class Constants
    {
        public const string DBConnectionFailed = "Failed to connect to database. Connection string was not specified.";
        public const string UsernameIsTaken = "This username is already taken. Try anotherone.";
        public const string EmailIsTaken = "This email is already taken. Try anotherone.";
        public const string FailedToCreateAccount = "An internal error occured! Failed to create account, please try again later.";
        public const string AccountCreated = "{{FULLNAME}}({{USERNAME}}) Created account successfully.";
        public const string InvalidEmail = "Enter valid email address.";
    }
}

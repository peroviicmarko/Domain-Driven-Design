using System.Net.Mail;

namespace DDD.API.Helpers
{
    public class DataValidation
    {
        public static bool ValidateEmail(string email)
        {
            try
            {
                MailAddress address = new MailAddress(email);
                return address.Address.Equals(email);
            }
            catch
            {
                return false;
            }
        }
    }
}

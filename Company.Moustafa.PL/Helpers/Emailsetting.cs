using System.Net;
using System.Net.Mail;

namespace Company.Moustafa.PL.Helpers
{
    public static class Emailsetting
    {
        public static bool SendEmail(Email email )
        
        {
            // Mail Server : Gmail 
            // SMTP : Simple Mail Transfare Protocol 

            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("moustafaelzan6@gmail.com", "ezrjccfdilkvdlmw");
                client.Send("moustafaelzan6@gmail.com", email.To, email.Subject, email.Body);
                return true;

            }
            catch( Exception ex ) 
            { 
                return false;
            }

        }
    }
}

using AJKM_phase1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace AJKM_phase1.Business_Logic
{
    public class MailHelper
    {
        public const string SUCCESS
        = "Success! Your email has been sent.  Please allow up to 48 hrs for a reply.";
        const string TO = "andrew.hui09@gmail.com"; // Specify where you want this email sent.
                                                    // This value may/may not be constant.
                                                    // To get started use one of your email 
                                                    // addresses.
        public string EmailFromArvixe(RegisteredUser message)
        {

            // Use credentials of the Mail account that you created with the steps above.
            const string FROM = "mail@ahui09.com";
            const string FROM_PWD = "keyaniscool";
            const bool USE_HTML = true;

            // Get the mail server obtained in the steps described above.
            const string SMTP_SERVER = "143.95.249.35";
            try
            {
                MailMessage mailMsg = new MailMessage(FROM, message.Email);
                mailMsg.Subject = message.Subject;
                mailMsg.Body = message.Body;
                mailMsg.IsBodyHtml = USE_HTML;

                SmtpClient smtp = new SmtpClient();
                smtp.Port = 25;
                smtp.Host = SMTP_SERVER;
                smtp.Credentials = new System.Net.NetworkCredential(FROM, FROM_PWD);
                smtp.Send(mailMsg);
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
            return SUCCESS;
        }
    }
}
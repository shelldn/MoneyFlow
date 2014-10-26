using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace MoneyFlow.Identity
{
    public sealed class MoneyFlowMailService : IIdentityMessageService
    {
        public SmtpClient MailClient { get; private set; }

        public MoneyFlowMailService()
        {
            var id = new NetworkCredential("nuka.lorenzo@gmail.com", "~2T[-+rJ^Q\f6T4");

            MailClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = id
            };
        }

        public Task SendAsync(IdentityMessage message)
        {
            var mail = new MailMessage("admin@mf.net",
                to: message.Destination,
                subject: message.Subject,
                body: message.Body

            ) { IsBodyHtml = true };

            return MailClient.SendMailAsync(mail);
        }
    }
}
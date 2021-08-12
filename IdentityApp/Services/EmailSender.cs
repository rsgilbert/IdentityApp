using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace IdentityApp.Services
{
    // summary
    //   Implements Identity's email sending service.
    //   Uses SendGrid.
    // See: https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-5.0&tabs=netcore-cli
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        // set options only via Secret Manager
        public AuthMessageSenderOptions Options { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            SendGridClient client = new SendGridClient(apiKey);
            SendGridMessage msg = new SendGridMessage()
            {
                From = new EmailAddress("admin@identityapp.com", Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking
            msg.SetClickTracking(false, false);
            return client.SendEmailAsync(msg);

        }
    }
}
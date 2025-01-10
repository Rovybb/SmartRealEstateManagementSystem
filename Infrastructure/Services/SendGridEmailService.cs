using SendGrid;
using SendGrid.Helpers.Mail;
using Application.Interfaces;
using System.Threading.Tasks;

public class SendGridEmailService : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string plainTextContent, string htmlContent = null)
    {
        var apiKey = "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"; // Replace with or retrieve from config
        var client = new SendGridClient(apiKey);

        var from = new EmailAddress("flaviburca@gmail.com", "Smart Real Estate Management System");
        var toDetails = new EmailAddress(to, "Agent");

        // If you want the HTML content as well, pass it as the fifth argument.
        // The third argument is the subject, the fourth is the plain text content,
        // and the fifth is the HTML content.
        var msg = MailHelper.CreateSingleEmail(from, toDetails, subject, plainTextContent, htmlContent);

        var response = await client.SendEmailAsync(msg);
        // Optionally, handle response status or errors here
    }
}

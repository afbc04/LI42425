using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService
{
    private readonly string _smtpServer;
    private readonly int _port;
    private readonly string _senderEmail;
    private readonly string _senderPassword;

    public EmailService(string smtpServer, int port, string senderEmail, string senderPassword)
    {
        _smtpServer = smtpServer;
        _port = port;
        _senderEmail = senderEmail;
        _senderPassword = senderPassword;
    }

    public async Task SendEmailAsync(string recipientEmail, string subject, string body)
    {
        using (var client = new SmtpClient(_smtpServer, _port))
        {
            client.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_senderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true 
            };
            mailMessage.To.Add(recipientEmail);

            await client.SendMailAsync(mailMessage);
        }
    }
}


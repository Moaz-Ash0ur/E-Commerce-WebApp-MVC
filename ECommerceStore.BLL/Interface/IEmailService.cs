namespace ECommerceStore.BLL.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toName, string toEmail, string subject, string textContent);
    }
}
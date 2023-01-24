using DemoApplication.Contracts.Email;

namespace PrioniaApp.Services.Abstracts
{
    public interface IEmailService
    {
        public void Send(MessageDto messageDto);
    }
}

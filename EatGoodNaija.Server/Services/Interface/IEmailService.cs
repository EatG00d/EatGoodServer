using EatGoodNaija.Server.Model.DTO;

namespace EatGoodNaija.Server.Services.Interface
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}

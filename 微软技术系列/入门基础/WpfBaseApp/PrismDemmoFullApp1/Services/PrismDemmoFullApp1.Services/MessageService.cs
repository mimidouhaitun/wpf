using PrismDemmoFullApp1.Services.Interfaces;

namespace PrismDemmoFullApp1.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}

namespace FintechGrupo10.Application.Comum.Services
{
    public interface IMessagePublisherService
    {
        void PublishMessage(string message, string queueName);
    }
}

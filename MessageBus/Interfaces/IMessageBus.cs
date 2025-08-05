namespace APIGestaoPedidos.MessageBus.Interfaces
{
    public interface IMessageBus
    {
        Task PublicarAsync<T> (T message, string queueName);
    }
}

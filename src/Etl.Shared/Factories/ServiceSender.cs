namespace Etl.Shared.Factories
{
    public class ServiceSender : ISender
    {
        private readonly IInputConnector _service;

        public ServiceSender(IInputConnector service)
        {
            _service = service;
        }
        public void Send(string content)
        {
            _service.Recive(content);
        }
    }
}
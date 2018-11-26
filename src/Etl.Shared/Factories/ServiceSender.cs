namespace Etl.Shared.Factories
{
    public class ServiceSender : ISender
    {
        private readonly IInputConnector _service;
        private readonly WorkMode _workMode;

        public ServiceSender(IInputConnector service, WorkMode workMode)
        {
            _service = service;
            _workMode = workMode;
        }
        public void Send(string content)
        {
            _service.Recive(content);
        }
    }
}
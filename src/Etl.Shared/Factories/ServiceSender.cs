using System.Threading.Tasks;

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
        public async Task Send(string content)
        {
            if (string.IsNullOrEmpty(content)) return;
            await _service.Recive(content);
        }
    }
}
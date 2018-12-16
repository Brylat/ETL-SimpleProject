using System.Threading.Tasks;

namespace Etl.Shared.Factories
{
    public class ServiceSender : ISender
    {
        private readonly IInputConnector _service;

        public ServiceSender(IInputConnector service)
        {
            _service = service;
        }
        public async Task Send(string content)
        {
            if (string.IsNullOrEmpty(content)) return;
            await _service.Recive(content);
        }
    }
}
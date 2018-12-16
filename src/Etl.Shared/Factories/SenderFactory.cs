using System.Threading.Tasks;
using Etl.Shared;

namespace Etl.Shared.Factories
{
    public class SenderFactory
    {
        private readonly WorkMode _workMode;
        private readonly string _path;
        private readonly IInputConnector _service;
        public SenderFactory(WorkMode workMode, string path, IInputConnector service) {
            _workMode = workMode;
            _path = path;
            _service = service;
        }

        public async Task<ISender> GetSender() {
            ISender sender = null;
            switch(_workMode) {
                case WorkMode.Continuous:
                    sender = new ServiceSender(_service);
                    break;
                case WorkMode.Partial:
                    sender = new FileSender(_path);
                    break;
                default:
                    break;
            }
            return await Task.FromResult(sender);
        }
    }
}
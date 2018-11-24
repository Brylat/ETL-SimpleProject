using Etl.Shared;

namespace Etl.Shared.Factories
{
    public class SenderFactory
    {
        private readonly WorkMode _workMode;
        private readonly string _path;
        public SenderFactory(WorkMode workMode, string path) {
            _workMode = workMode;
            _path = path;
        }

        public ISender GetSender() {
            ISender sender = null;
            switch(_workMode) {
                case WorkMode.Continuous:
                    sender = new ServiceSender();
                    break;
                case WorkMode.Partial:
                    sender = new FileSender(_path);
                    break;
                default:
                    break;
            }
            return sender;
        }
    }
}
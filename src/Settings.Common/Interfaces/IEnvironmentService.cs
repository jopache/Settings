
using Settings.Common.Domain;

namespace Settings.Common.Interfaces
{
    public interface IEnvironmentService
    {
        Environment AddEnvironment(Environment environment, int parentEnvironmentId);
    }
}

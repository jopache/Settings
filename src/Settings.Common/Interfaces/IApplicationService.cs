
using Settings.Common.Domain;

namespace Settings.Common.Interfaces
{
    public interface IApplicationService
    {
        void AddApplication(Application application, int parentApplicationId);
    }
}

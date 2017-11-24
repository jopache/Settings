
using Settings.Common.Domain;

namespace Settings.Common.Interfaces
{
    public interface IApplicationService
    {
        Application AddApplication(Application application, int parentApplicationId);
    }
}

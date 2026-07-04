using Slope.Models;

namespace Slope.Services
{
    public interface IProjectService
    {
        CalculationInput LoadInput(string projectFolder);

        void SaveInput(MenuContext context);
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace Slope.Services;

public static class ServiceFactory
{
    static ServiceCollection _services = new ServiceCollection();
    static ServiceProvider _provider;

    static ServiceFactory()
    {
        _services.AddSingleton<IProjectService, ProjectService>();

        _provider = _services.BuildServiceProvider();
    }

    public static T Get<T>()
    {
        return _provider.GetService<T>();
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Excercise5Garage
{
    public class StartUp
    {
        public void SetUp()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.GetService<GarageProgram>().Run();
        }


        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<GarageProgram>();
        }
    }
}

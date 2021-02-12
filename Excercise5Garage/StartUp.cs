using Excercise5Garage.Menu;
using Excercise5Garage.Menu.Interface;
using Excercise5Garage.RegistrationNumber;
using Excercise5Garage.RegistrationNumber.Interface;
using Excercise5Garage.UI;
using Excercise5Garage.UI.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;

// [assembly:InternalsVisibleTo("NUnitExcercise5Garage.Tests")] Om man har internal klasser som man vill testa
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
            services.AddSingleton<IMenuFactory, MenuFactory>();
            services.AddSingleton<IUI, ConsoleUI>();
            services.AddSingleton<IRegistrationNumberRegister, RegistrationNumberRegister>();
        }
    }
}

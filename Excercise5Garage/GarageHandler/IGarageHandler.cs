using Excercise5Garage.Garage;
using Excercise5Garage.UI;

namespace Excercise5Garage.GarageHandler
{
    public interface IGarageHandler
    {
        IGarage<ICanBeParkedInGarage> Garage { get; }
        IUI Ui { get; }

        bool ParkVehicle(ICanBeParkedInGarage vehicle);

        void PrintInformation();
    }
}
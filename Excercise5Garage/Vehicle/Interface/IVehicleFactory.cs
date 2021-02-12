using Excercise5Garage.Garage.Interface;

namespace Excercise5Garage.Vehicle.Interface
{
    public interface IVehicleFactory
    {
        ICanBeParkedInGarage CreateRandomVehicleForGarage();
    }
}
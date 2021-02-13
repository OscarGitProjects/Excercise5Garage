using Excercise5Garage.Garage.Interface;

namespace Excercise5Garage.Vehicle.Interface
{
    public interface IVehicleFactory
    {
        (string strColor, int iNumberOfWheels, int iNumberOfSeatedPassengers) GetDefaultVehicleData(Vehicle_Type enumVehicleType);
        ICanBeParkedInGarage CreateVehicle(Vehicle_Type enumVehicleType, string strRegistrationNumber, string strColor, int iNumberOfWheels, int iNumberOfSeatedPassengers);
        ICanBeParkedInGarage CreateRandomVehicleForGarage();
    }
}
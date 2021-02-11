using Excercise5Garage.Garage.Interface;
using Excercise5Garage.UI.Interface;

namespace Excercise5Garage.GarageHandler.Interface
{
    public interface IGarageHandler
    {
        IGarage<ICanBeParkedInGarage> Garage { get; set; }
        IUI Ui { get; }
        bool ParkVehicle(ICanBeParkedInGarage vehicle);
        bool RemoveVehicle(ICanBeParkedInGarage vehicle);
        void PrintInformation();
        (string strId, string strName, bool bIsFull) GetGarageInfo();
    }
}
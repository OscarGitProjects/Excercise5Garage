using Excercise5Garage.Garage.Interface;
using Excercise5Garage.UI.Interface;
using System;

namespace Excercise5Garage.GarageHandler.Interface
{
    public interface IGarageHandler
    {
        IGarage<ICanBeParkedInGarage> Garage { get; set; }
        IUI Ui { get; }
        Guid GuidId { get; }
        bool ParkVehicle(ICanBeParkedInGarage vehicle);
        bool RemoveVehicle(ICanBeParkedInGarage vehicle);
        bool RemoveVehicle(int iIndex);
        void PrintAllInformationAboutGarage();
        void PrintInformationAboutGarage();
        (string strId, string strName, bool bIsFull, int iCapacity, int iNumberOfParkedVehicle) GetGarageInfo();
        int CountVehicleWithRegistrationNumber(string strRegistrationNumber);
    }
}
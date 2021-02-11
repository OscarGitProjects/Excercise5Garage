using Excercise5Garage.Garage;
using Excercise5Garage.Garage.Interface;
using Excercise5Garage.Vehicle.WheeledVehicle;
using System;

namespace Excercise5Garage.Vehicle
{
    public enum Vehicle_Type
    {
        NA = 0,
        CAR = 1,
        BUS = 2,
        MOTORCYCLE = 3
    }

    public class VehicleFactory
    {
        public ICanBeParkedInGarage CreateRandomVehicleForGarage()
        {
            ICanBeParkedInGarage vehicle = null;
            Random rand = new Random();
            int iRandomCarType = rand.Next(1, 4);

            switch(iRandomCarType)
            {
                case 1:         // Car
                    vehicle = new Car("AAA 111", "Vit", 4, 5);
                    break;
                case 2:         // Bus
                    vehicle = new Bus("BBB 222", "Vit", 4, 50);
                    break;
                case 3:         // Motorcycle
                    vehicle = new MotorCycle("CCC 333", "Vit", 2, 2);
                    break;
            }

            return vehicle;
        }

    }
}
using Excercise5Garage.Garage.Interface;
using Excercise5Garage.RegistrationNumber.Interface;
using Excercise5Garage.Vehicle.Interface;
using Excercise5Garage.Vehicle.WheeledVehicle;
using System;
using System.Collections.Generic;

namespace Excercise5Garage.Vehicle
{
    public enum Vehicle_Type
    {
        NA = 0,
        CAR = 1,
        BUS = 2,
        MOTORCYCLE = 3
    }

    public class VehicleFactory : IVehicleFactory
    {
        /// <summary>
        /// Register där använda registreringsnummer finns lagrade
        /// </summary>
        private IRegistrationNumberRegister RegistrationNumberRegister { get; }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="registrationNumberRegister">Referense till RegistrationNumberRegister där använda registreringsnummer sparas</param>
        public VehicleFactory(IRegistrationNumberRegister registrationNumberRegister)
        {
            RegistrationNumberRegister = registrationNumberRegister;
        }


        /// <summary>
        /// Metoden skapar ett slumpmässigt fordon som kan parkeras i ett garage dvs. måste implemnetera interfacet ICanBeParkedInGarage
        /// </summary>
        /// <returns>Nytt fordon som kan parkeras i ett garage</returns>
        public ICanBeParkedInGarage CreateRandomVehicleForGarage()
        {
            ICanBeParkedInGarage vehicle = null;
            Random rand = new Random();

            // Skapa ett nytt registreringsnummer som inte används
            string strRegistrationNumber = this.RegistrationNumberRegister.CreateRandomRegistrationNumber();
            // Skapa en ny färg
            string strColor = this.CreateRandomColor();
            // Vad skall vi har för typ av fordon
            int iRandomCarType = rand.Next(1, 4);

            switch (iRandomCarType)
            {
                case 1:         // Car
                    vehicle = new Car(strRegistrationNumber, strColor, 4, 5);
                    break;
                case 2:         // Bus
                    vehicle = new Bus(strRegistrationNumber, strColor, 4, rand.Next(20, 76));
                    break;
                case 3:         // Motorcycle
                    vehicle = new MotorCycle(strRegistrationNumber, strColor, 2, 2);
                    break;
            }

            return vehicle;
        }


        /// <summary>
        /// Metoden skapar en slumpmässig färg. Färgens namn returneras som en string
        /// </summary>
        /// <returns>En färgs namn</returns>
        private string CreateRandomColor()
        {
            Random rand = new Random();

            // Skapa en dictionary med olika färger
            Dictionary<int, string> dicColors = new Dictionary<int, string>();
            dicColors.Add(1, "Svart");
            dicColors.Add(2, "Vit");
            dicColors.Add(3, "Röd");
            dicColors.Add(4, "Grön");
            dicColors.Add(5, "Blå");
            dicColors.Add(6, "Brun");
            dicColors.Add(7, "Gul");
            dicColors.Add(8, "Lila");
            dicColors.Add(9, "Orange");

            int iRandomColor = rand.Next(1, 10);
            string strColor = dicColors[iRandomColor];

            return strColor;
        }
    }
}
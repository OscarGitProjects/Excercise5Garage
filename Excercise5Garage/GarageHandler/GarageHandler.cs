using Excercise5Garage.Garage;
using Excercise5Garage.UI;
using Excercise5Garage.Vehicle;
using System;

namespace Excercise5Garage.GarageHandler
{
    public class GarageHandler : IGarageHandler
    {
        /// <summary>
        /// Garaga som denna handler hanterar
        /// </summary>
        public IGarage<ICanBeParkedInGarage> Garage { get; }

        /// <summary>
        /// Ui för utskrift och inmatning av kommandon och text
        /// </summary>
        public IUI Ui { get; }



        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="ui">Referens till ui</param>
        public GarageHandler(IUI ui)
        {
            Ui = ui;
        }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="garage">Referens till garage</param>
        /// <param name="ui">Referens till ui</param>
        public GarageHandler(IGarage<ICanBeParkedInGarage> garage, IUI ui)
        {
            Ui = ui;
            Garage = garage;
        }


        /// <summary>
        /// Metoden parkerar ett fordon i garaget
        /// </summary>
        /// <param name="vehicle">Fordonet som skall parkeras i garaget</param>
        /// <returns>true om det gick parkera fordonet. Annars returneras false</returns>
        /// <exception cref="System.ArgumentNullException">Kastas om referensen till vehicle är null</exception>
        /// <exception cref="System.NullReferenceException">Kastas om referensen till Garage är null</exception>
        public bool ParkVehicle(ICanBeParkedInGarage vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException("ArgumentNullException. GarageHandler.ParkVehicle(ICanBeParkedInGarage vehicle). Referensen till vehicle är null");

            if (Garage == null)
                throw new NullReferenceException("NullReferenceException. GarageHandler.ParkVehicle(ICanBeParkedInGarage vehicle). Garage referensen är null");


            bool bParkedVehicle = false;
            if (Garage.IsFull)
            {
                Ui.WriteLine($"{Garage.GarageName} är fullt");
            }
            else
            {// Vi kan försöka att parkera fordonet

                bParkedVehicle = Garage.Add(vehicle);
            }

            if (bParkedVehicle)
            {// Vi har parkerat ett fordon. Skriv ut info om detta

                var tmpVehicle = vehicle as IVehicle;
                string strRegistrationNumber = tmpVehicle?.RegistrationNumber;
                Ui.WriteLine($"Parkerar fordon {vehicle.GetType().Name} med registreringsnummer {strRegistrationNumber}");
            }

            return bParkedVehicle;
        }


        /// <summary>
        /// Metoden skriver ut all information om garaget och de parkerade fordonen
        /// </summary>
        public void PrintInformation()
        {
            if(Garage != null)
                Ui.WriteLine(Garage.PrintAllInformation());
        }


        /// <summary>
        /// Överlagring av ToString
        /// </summary>
        /// <returns>Namnet på det garage som hanteras</returns>
        public override string ToString()
        {
            string str = "Hanterar ";
            if(Garage != null)
                str += Garage.GarageName;

            return str;
        }
    }
}

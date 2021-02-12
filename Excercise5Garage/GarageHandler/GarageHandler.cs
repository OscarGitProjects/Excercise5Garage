using Excercise5Garage.Garage.Interface;
using Excercise5Garage.GarageHandler.Interface;
using Excercise5Garage.UI.Interface;
using Excercise5Garage.Vehicle.Interface;
using System;

namespace Excercise5Garage.GarageHandler
{
    public class GarageHandler : IGarageHandler
    {
        /// <summary>
        /// Garage som denna handler hanterar
        /// </summary>
        public IGarage<ICanBeParkedInGarage> Garage { get; set; }

        /// <summary>
        /// Ui för utskrift och inmatning av kommandon och text
        /// </summary>
        public IUI Ui { get; }

        /// <summary>
        /// Unik identifierare av garageHandler och Garage. Det är samma Guid som i Garage
        /// </summary>
        public Guid GuidId { get; }



        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="ui">Referens till ui</param>
        /// <param name="guid">Unikt id för garageHandler. Skall vara samma som i garage</param>
        public GarageHandler(IUI ui, Guid guid)
        {
            Ui = ui;
            GuidId = guid;
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
            GuidId = garage.GarageID;
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
        /// Metoden tar bort ett parkerat fordon från garaget
        /// </summary>
        /// <param name="vehicle">Fordonet som skall tas bort från garaget</param>
        /// <returns>true om det gick radera fordonet. Annars returneras false</returns>
        /// <exception cref="System.ArgumentNullException">Kastas om referensen till vehicle är null</exception>
        /// <exception cref="System.NullReferenceException">Kastas om referensen till Garage är null</exception>
        public bool RemoveVehicle(ICanBeParkedInGarage vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException("ArgumentNullException. GarageHandler.RemoveVehicle(ICanBeParkedInGarage vehicle). Referensen till vehicle är null");

            if (Garage == null)
                throw new NullReferenceException("NullReferenceException. GarageHandler.RemoveVehicle(ICanBeParkedInGarage vehicle). Garage referensen är null");


            var tmpVehicle = vehicle as IVehicle;
            string strRegistrationNumber = tmpVehicle?.RegistrationNumber;

            bool bRemovedVehicle = Garage.Remove(vehicle);
            if(bRemovedVehicle)
            {
                Ui.WriteLine($"Fordon {vehicle.GetType().Name} med registreringsnummer {strRegistrationNumber} har lämnat garaget");
            }
            else
            {
                Ui.WriteLine($"Fordon {vehicle.GetType().Name} med registreringsnummer {strRegistrationNumber} kan inte lämna garaget. Bilen finns troligen inte i garaget");
            }

            return bRemovedVehicle;
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


        /// <summary>
        /// Metoden returnera information om garagets unika id, namn och om garaget är fullt eller ej
        /// </summary>
        /// <returns>Returnera information om garagets unika id, namn och om garaget är fullt eller ej, kapacitet och antal vehicle som är parkerade</returns>
        /// <exception cref="System.NullReferenceException">Kastas om referensen till Garage är null</exception>
        public (string strId, string strName, bool bIsFull, int iCapacity, int iNumberOfParkedVehicle) GetGarageInfo()
        {
            if (Garage == null)
                throw new NullReferenceException("NullReferenceException. GarageHandler.GetGarageInfo(ICanBeParkedInGarage vehicle). Garage referensen är null");

            return (strId: Garage.GarageID.ToString(), strName: Garage.GarageName, bIsFull: Garage.IsFull, iCapacity: Garage.Capacity, iNumberOfParkedVehicle: Garage.Count);
        }


        /// <summary>
        /// Metoden räknar antalet vehicle som har sökt registreringsnummer
        /// Metoden tar inte hänsyn till om det är stora eller små bokstäver
        /// </summary>
        /// <param name="strRegistrationNumber">Registreringsnummer somm söks</param>
        /// <returns>Antalet vehicle med sökt registreringsnummer</returns>
        public int CountVehicleWithRegistrationNumber(string strRegistrationNumber)
        {
            if (Garage == null)
                throw new NullReferenceException("NullReferenceException. GarageHandler.CountVehicleWithRegistrationNumber(string strRegistrationNumber). Garage referensen är null");

            int iNumberOfVehicleWithRegistrationNumber = Garage.CountVehicleWithRegistrationNumber(strRegistrationNumber);
            return iNumberOfVehicleWithRegistrationNumber;
        }
    }
}

using Excercise5Garage.Garage;

namespace Excercise5Garage.Vehicle.WheeledVehicle
{
    public class Bus : WheeledVehicle, ICanBeParkedInGarage
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Bussens registreringsnummer</param>
        /// <param name="strColor">Bussens färg. Default värde är Svart</param>
        /// <param name="iNumberOfWheels">Antal hjul på bussen. Default värde är 4</param>
        public Bus(string strRegistrationNumber, string strColor = "Svart", int iNumberOfWheels = 4) : base(strRegistrationNumber, strColor, iNumberOfWheels)
        {
        }
    }
}

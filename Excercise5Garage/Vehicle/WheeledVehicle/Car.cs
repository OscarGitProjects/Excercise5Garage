using Excercise5Garage.Garage;

namespace Excercise5Garage.Vehicle.WheeledVehicle
{
    public class Car : WheeledVehicle, ICanBeParkedInGarage
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Bilens registreringsnummer</param>
        /// <param name="strColor">Bilens färg. Default värde är Svart</param>
        /// <param name="iNumberOfWheels">Antal hjul på bilen. Default värde är 4</param>
        public Car(string strRegistrationNumber, string strColor = "Svart", int iNumberOfWheels = 4) : base(strRegistrationNumber, strColor, iNumberOfWheels)
        {            
        }
    }
}

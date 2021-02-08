using Excercise5Garage.Garage;

namespace Excercise5Garage.Vehicle.WheeledVehicle
{
    public class MotorCycle : WheeledVehicle, ICanBeParkedInGarage
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Motorcykelns registreringsnummer</param>
        /// <param name="strColor">Notorcykelns färg. Default värde är Svart</param>
        /// <param name="iNumberOfWheels">Antal hjul på motorcykeln. Default värde är 2</param>
        public MotorCycle(string strRegistrationNumber, string strColor = "Svart", int iNumberOfWheels = 2) : base(strRegistrationNumber, strColor, iNumberOfWheels)
        {
        }
    }
}

namespace Excercise5Garage.Vehicle.AirVehicle
{
    public class AirVehicle : Vehicle
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Fordonets registreringsnummer</param>
        /// <param name="strColor">Fordonets färg</param>
        public AirVehicle(string strRegistrationNumber, string strColor) : base(strRegistrationNumber, strColor)
        {
        }
    }
}

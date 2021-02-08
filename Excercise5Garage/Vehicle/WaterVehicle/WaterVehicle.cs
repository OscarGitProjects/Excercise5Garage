namespace Excercise5Garage.Vehicle.WaterVehicle
{
    /// <summary>
    /// Basklass för fordon som färdas i vatten ex segelbåt, motorbåt
    /// </summary>
    public class WaterVehicle : Vehicle
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Fordonets registreringsnummer</param>
        /// <param name="strColor">Fordonets färg</param>
        public WaterVehicle(string strRegistrationNumber, string strColor) : base(strRegistrationNumber, strColor)
        {
        }
    }
}

namespace Excercise5Garage.Vehicle
{
    /// <summary>
    /// Abstract bassklass för olika fordon
    /// </summary>
    public abstract class Vehicle : IVehicle
    {
        /// <summary>
        /// Fordonets registreringsnummer
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Fordonets färg
        /// </summary>
        public string Color { get; set; }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Fordonets registreringsnummer</param>
        /// <param name="strColor">Fordonets färg</param>
        /// <param name="iNumberOfWheels">Antal hjul på Fordonet</param>
        public Vehicle(string strRegistrationNumber, string strColor)
        {
            RegistrationNumber = strRegistrationNumber;
            Color = strColor;
        }
    }
}

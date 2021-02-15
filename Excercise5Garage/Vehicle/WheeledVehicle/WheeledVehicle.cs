namespace Excercise5Garage.Vehicle.WheeledVehicle
{
    /// <summary>
    /// Bassklass för fordon som framförs på hjul ex. bil, buss, motorcykel
    /// </summary>
    public class WheeledVehicle : Vehicle
    {
        /// <summary>
        /// Antal hjul på fordonet
        /// </summary>
        public int NumberOfWheels { get; set; }

        /// <summary>
        /// Antalet sittande passagerar som fordonet kan ta
        /// </summary>
        public int NumberOfSeatedPassengers { get; set; }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Fordonets registreringsnummer</param>
        /// <param name="strColor">Fordonets färg</param>
        /// <param name="iNumberOfWheels">Antal hjul på Fordonet</param>
        /// <param name="iNumberOfSeatedPassengers">Antal sittande passagerare</param>
        public WheeledVehicle(string strRegistrationNumber, string strColor, int iNumberOfWheels, int iNumberOfSeatedPassengers) : base(strRegistrationNumber, strColor)
        {
            this.NumberOfWheels = iNumberOfWheels;
            this.NumberOfSeatedPassengers = iNumberOfSeatedPassengers;
        }
    }
}
namespace Excercise5Garage.Vehicle
{
    public class Vehicle
    {
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Fordonets registreringsnummer</param>
        /// <param name="strColor">Fordonets färg</param>
        /// <param name="iNumberOfWheels">Antal hjul på Fordonet</param>
        public Vehicle(string strRegistrationNumber, string strColor)
        {
            this.RegistrationNumber = strRegistrationNumber;
            this.Color = strColor;
        }
    }
}

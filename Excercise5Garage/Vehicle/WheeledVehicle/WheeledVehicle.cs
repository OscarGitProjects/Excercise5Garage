namespace Excercise5Garage.Vehicle.WheeledVehicle
{
    public class WheeledVehicle : Vehicle
    {
        public int NumberOfWheels { get; set; }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Fordonets registreringsnummer</param>
        /// <param name="strColor">Fordonets färg</param>
        /// <param name="iNumberOfWheels">Antal hjul på Fordonet</param>
        public WheeledVehicle(string strRegistrationNumber, string strColor, int iNumberOfWheels): base(strRegistrationNumber, strColor)
        {
            this.NumberOfWheels = iNumberOfWheels;
        }
    }
}
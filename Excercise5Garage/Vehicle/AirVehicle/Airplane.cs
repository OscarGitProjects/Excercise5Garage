namespace Excercise5Garage.Vehicle.AirVehicle
{
    public class Airplane : AirVehicle
    {
        /// <summary>
        /// Antalet motorer som flygplanet har
        /// </summary>
        public int NumberOfEngines { get; set; }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Flygplanets registreringsnummer</param>
        /// <param name="strColor">Flygplanets färg. Default värde är Svart</param>
        /// <param name="iNumberOfEngins">Antalet motorer. Default värde är 2</param>
        public Airplane(string strRegistrationNumber, string strColor = "Svart", int iNumberOfEngines = 2) : base(strRegistrationNumber, strColor)
        {
            NumberOfEngines = iNumberOfEngines;
        }
    }
}

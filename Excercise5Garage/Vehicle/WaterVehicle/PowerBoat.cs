namespace Excercise5Garage.Vehicle.WaterVehicle
{
    public class PowerBoat : WaterVehicle
    {
        /// <summary>
        /// Antalet motorer som Motorbåtens har
        /// </summary>
        public int NumberOfEngines { get; set; }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Motorbåtens registreringsnummer</param>
        /// <param name="strColor">Motorbåtens färg. Default värde är Svart</param>
        /// <param name="iNumberOfEngines"></param>
        /// <param name="iNumberOfEngins">Antalet motorer. Default värde är 2</param>
        public PowerBoat(string strRegistrationNumber, string strColor = "Svart", int iNumberOfEngines = 2) : base(strRegistrationNumber, strColor)
        {
            this.NumberOfEngines = iNumberOfEngines;
        }
    }
}

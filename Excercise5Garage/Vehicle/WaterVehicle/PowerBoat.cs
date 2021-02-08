namespace Excercise5Garage.Vehicle.WaterVehicle
{
    /// <summary>
    /// Klass med information om en motorbåt
    /// </summary>
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


        public override string ToString()
        {
            return $"{this.GetType().Name}. Registreringsnummer: {this.RegistrationNumber}, Färg: {this.Color}, Antal motorer: {this.NumberOfEngines}";
        }
    }
}

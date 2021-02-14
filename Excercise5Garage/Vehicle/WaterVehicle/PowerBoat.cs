using Excercise5Garage.Extensions;

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
        public PowerBoat(string strRegistrationNumber, string strColor = "SVART", int iNumberOfEngines = 2) : base(strRegistrationNumber, strColor)
        {
            this.NumberOfEngines = iNumberOfEngines;
        }


        /// <summary>
        /// Överlagring av ToString()
        /// </summary>
        /// <returns>String med information om objektet</returns>
        public override string ToString()
        {
            return $"{this.GetType().Name}. Registreringsnummer: {this.RegistrationNumber}, Färg: {this.Color?.ToLower()?.FirstToUpper()}, Antal motorer: {this.NumberOfEngines}";
        }
    }
}

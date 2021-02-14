using Excercise5Garage.Extensions;

namespace Excercise5Garage.Vehicle.WaterVehicle
{
    /// <summary>
    /// Klass med information om en segelbåt
    /// </summary>
    public class SailingBoat : WaterVehicle
    {
        /// <summary>
        /// Antalet motorer som segelbåten har
        /// </summary>
        public int NumberOfEngines { get; set; }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Segelbåtens registreringsnummer</param>
        /// <param name="strColor">Segelbåtens färg. Default värde är Svart</param>
        /// <param name="iNumberOfEngins">Antalet motorer. Default värde är 1</param>
        public SailingBoat(string strRegistrationNumber, string strColor = "SVART", int iNumberOfEngines = 1) : base(strRegistrationNumber, strColor)
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

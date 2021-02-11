using Excercise5Garage.Vehicle.Interface;

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



        /// <summary>
        /// Överlagring av metoden Equals
        /// </summary>
        /// <param name="obj">Objekt som vi skall jämföra med</param>
        /// <returns>true om objekten är lika. Annars returneras false</returns>
        public override bool Equals(object obj)
        {
            Vehicle vehicle = obj as Vehicle;

            if (vehicle != null)
                return (string.Compare(this.RegistrationNumber, vehicle.RegistrationNumber, true) == 0) ? true : false;
            
            return base.Equals(obj);
        }


        /// <summary>
        /// Överlagring av metoden GetHashCode
        /// </summary>
        /// <returns>Returnerar HashCode för registreringsnumret</returns>
        public override int GetHashCode()
        {
            return this.RegistrationNumber.GetHashCode();
        }
    }
}

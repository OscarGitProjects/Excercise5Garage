using Excercise5Garage.Garage;

namespace Excercise5Garage.Vehicle.WheeledVehicle
{
    /// <summary>
    /// Klass med information om en motorcykel
    /// </summary>
    public class MotorCycle : WheeledVehicle, ICanBeParkedInGarage
    {
        /// <summary>
        /// Antalet sittande passagerar som fordonet kan ta
        /// </summary>
        public int NumberOfSeatedPassengers { get; set; }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Motorcykelns registreringsnummer</param>
        /// <param name="strColor">Notorcykelns färg. Default värde är Svart</param>
        /// <param name="iNumberOfWheels">Antal hjul på motorcykeln. Default värde är 2</param>
        public MotorCycle(string strRegistrationNumber, string strColor = "Svart", int iNumberOfWheels = 2) : base(strRegistrationNumber, strColor, iNumberOfWheels)
        {
            NumberOfSeatedPassengers = 2;
        }



        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Motorcykelns registreringsnummer</param>
        /// <param name="strColor">Notorcykelns färg. Default värde är Svart</param>
        /// <param name="iNumberOfWheels">Antal hjul på motorcykeln. Default värde är 2</param>
        /// <param name="iNumberOfSeatedPassengers">Antal sittande passagerar som fordonet kan ta. Default är 2</param>
        public MotorCycle(string strRegistrationNumber, string strColor = "Svart", int iNumberOfWheels = 2, int iNumberOfSeatedPassengers = 2) : base(strRegistrationNumber, strColor, iNumberOfWheels)
        {
            NumberOfSeatedPassengers = iNumberOfSeatedPassengers;
        }


        public override string ToString()
        {
            return $"{this.GetType().Name}. Registreringsnummer: {this.RegistrationNumber}, Färg: {this.Color}, Antal hjul: {this.NumberOfWheels}, Antal sittande passagerare: {this.NumberOfSeatedPassengers}";
        }
    }
}

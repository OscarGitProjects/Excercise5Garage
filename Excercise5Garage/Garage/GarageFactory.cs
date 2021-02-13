using Excercise5Garage.Garage.Interface;
using System;

namespace Excercise5Garage.Garage
{
    /// <summary>
    /// Klassen har metoder för att skapa Garage
    /// </summary>
    public class GarageFactory : IGarageFactory
    {
        /// <summary>
        /// Metoden skapar ett garage
        /// </summary>
        /// <param name="guid">Unikt id för garaget</param>
        /// <param name="strGarageName">Garagets namn</param>
        /// <param name="iCapacity">Garagets kapacitet dvs. hur många fordon som kan parkeras i garaget</param>
        /// <returns>Garage objekt</returns>
        public IGarage<ICanBeParkedInGarage> CreateGarage(Guid guid, string strGarageName, int iCapacity)
        {
            return new Garage<ICanBeParkedInGarage>(guid, strGarageName, iCapacity);
        }
    }
}

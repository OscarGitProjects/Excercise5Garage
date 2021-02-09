using Excercise5Garage.Vehicle;

namespace Excercise5Garage.Garage
{
    public class GarageHandler
    {
        public IGarage<IVehicle> Garage { get; set; }


        // TODO GÖRA KLART

        public GarageHandler(IGarage<IVehicle> garage)
        {
            Garage = garage;
        }
    }
}

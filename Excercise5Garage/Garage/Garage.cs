namespace Excercise5Garage.Garage
{
    public class Garage<T> where T : ICanBeParkedInGarage
    {
        private T[] arrVehicles;
    }
}

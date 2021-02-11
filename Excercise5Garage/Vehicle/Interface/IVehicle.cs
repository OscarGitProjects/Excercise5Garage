namespace Excercise5Garage.Vehicle.Interface
{
    /// <summary>
    /// Interface för olika fordon
    /// </summary>
    public interface IVehicle
    {
        string RegistrationNumber { get; set; }
        string Color { get; set; }
    }
}

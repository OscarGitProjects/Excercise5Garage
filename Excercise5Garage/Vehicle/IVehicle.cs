namespace Excercise5Garage.Vehicle
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

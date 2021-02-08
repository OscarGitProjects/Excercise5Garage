using Excercise5Garage.Vehicle;
using Excercise5Garage.Vehicle.AirVehicle;
using NUnit.Framework;


namespace NUnitExcercise5Garage.Tests
{

    /// <summary>
    /// Klassen testar klasser som ärver från AirVehicle
    /// </summary>
    public class AirVehicleTests
    {

        [SetUp]
        public void Setup()
        {
        }


        [Test]
        [Description("Metoden testar Airplane klassens konstruktor fungerar")]
        public void PowerBoat_Constructor_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            IVehicle vehicle = new Airplane("ABC 123", "Röd", 2);


            // Act
            // actual
            string strActualRegistrationNumber = ((Vehicle)vehicle).RegistrationNumber;
            string strActualColor = ((Vehicle)vehicle).Color;
            int iActualNumberOfEngins = ((Airplane)vehicle).NumberOfEngines;


            // Assert
            Assert.AreEqual("ABC 123", strActualRegistrationNumber);

            Assert.AreEqual("Röd", strActualColor);

            Assert.AreEqual(2, iActualNumberOfEngins);
        }
    }
}

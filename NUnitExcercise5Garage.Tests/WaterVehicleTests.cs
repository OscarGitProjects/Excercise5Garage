using Excercise5Garage.Vehicle;
using Excercise5Garage.Vehicle.Interface;
using Excercise5Garage.Vehicle.WaterVehicle;
using NUnit.Framework;

namespace NUnitExcercise5Garage.Tests
{
    /// <summary>
    /// Klassen testar klasser som ärver från WaterVehicle
    /// </summary>
    public class WaterVehicleTests
    {
        [SetUp]
        public void Setup()
        {
        }



        /// <summary>
        /// Metoden testar PowerBoat klassens konstruktor fungerar
        /// </summary>
        [Test]
        [Description("Metoden testar PowerBoat klassens konstruktor fungerar")]
        public void PowerBoat_Constructor_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            IVehicle vehicle = new PowerBoat("ABC 123", "Röd", 2);


            // Act
            // actual
            string strActualRegistrationNumber = ((Vehicle)vehicle).RegistrationNumber;
            string strActualColor = ((Vehicle)vehicle).Color;
            int iActualNumberOfEngins = ((PowerBoat)vehicle).NumberOfEngines;


            // Assert
            Assert.AreEqual("ABC 123", strActualRegistrationNumber);

            Assert.AreEqual("Röd", strActualColor);

            Assert.AreEqual(2, iActualNumberOfEngins);
        }



        /// <summary>
        /// Metoden testar SailingBoat klassens konstruktor fungerar
        /// </summary>
        [Test]
        [Description("Metoden testar SailingBoat klassens konstruktor fungerar")]
        public void SailingBoat_Constructor_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            IVehicle vehicle = new SailingBoat("ABC 123", "Röd", 2);


            // Act
            // actual
            string strActualRegistrationNumber = ((Vehicle)vehicle).RegistrationNumber;
            string strActualColor = ((Vehicle)vehicle).Color;
            int iActualNumberOfEngins = ((SailingBoat)vehicle).NumberOfEngines;


            // Assert
            Assert.AreEqual("ABC 123", strActualRegistrationNumber);

            Assert.AreEqual("Röd", strActualColor);

            Assert.AreEqual(2, iActualNumberOfEngins);
        }
    }
}

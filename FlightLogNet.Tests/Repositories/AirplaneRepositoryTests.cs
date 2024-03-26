namespace FlightLogNet.Tests.Repositories
{
    using AutoMapper;

    using Models;
    using FlightLogNet.Repositories;

    using Xunit;

    using Microsoft.Extensions.Configuration;

    public class AirplaneRepositoryTests(IMapper mapper, IConfiguration configuration)
    {
        private AirplaneRepository CreateAirplaneRepository()
        {
            return new AirplaneRepository(mapper, configuration);
        }

        private void RenewDatabase()
        {
            TestDatabaseGenerator.RenewDatabase(configuration);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void AddGuestAirplane_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            this.RenewDatabase();
            var airplaneRepository = this.CreateAirplaneRepository();
            AirplaneModel airplaneModel = new AirplaneModel
            {
                Immatriculation = "OKA-424",
                Type = "Zlín"
            };

            // Act
            var result = airplaneRepository.AddGuestAirplane(airplaneModel);

            // Assert
            Assert.True(result > 0, "There should be Id (> 0) of new guest airplane.");
        }

        [Fact]
        public void GetClubAirplanes_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            this.RenewDatabase();
            var airplaneRepository = this.CreateAirplaneRepository();

            // Act
            var result = airplaneRepository.GetClubAirplanes();

            // Assert
            Assert.NotEmpty(result);
        }
    }
}

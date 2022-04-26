namespace FlightLogNet.Tests.Repositories
{
    using AutoMapper;

    using FlightLogNet.Models;
    using FlightLogNet.Repositories;

    using Xunit;

    using Microsoft.Extensions.Configuration;

    public class AirplaneRepositoryTests
    {
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AirplaneRepositoryTests(IMapper mapper, IConfiguration configuration)
        {
            this.mapper = mapper;
            this.configuration = configuration;
        }

        private AirplaneRepository CreateAirplaneRepository()
        {
            return new AirplaneRepository(mapper, this.configuration);
        }

        private void RenewDatabase()
        {
            TestDatabaseGenerator.RenewDatabase(this.configuration);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void AddGuestAirplane_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            RenewDatabase();
            var airplaneRepository = CreateAirplaneRepository();
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
            RenewDatabase();
            var airplaneRepository = CreateAirplaneRepository();

            // Act
            var result = airplaneRepository.GetClubAirplanes();

            // Assert
            Assert.NotEmpty(result);
        }
    }
}

namespace FlightLogNet.Tests.Repositories
{
    using AutoMapper;

    using FlightLogNet.Models;
    using FlightLogNet.Repositories;

    using Xunit;

    public class AirplaneRepositoryTests
    {
        private readonly IMapper mapper;

        public AirplaneRepositoryTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        private AirplaneRepository CreateAirplaneRepository()
        {
            return new AirplaneRepository(mapper);
        }

        private void RenewDatabase()
        {
            TestDatabaseGenerator.RenewDatabase();
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

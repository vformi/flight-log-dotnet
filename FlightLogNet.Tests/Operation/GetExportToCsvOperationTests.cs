namespace FlightLogNet.Tests.Operation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using FlightLogNet.Models;
    using FlightLogNet.Operation;
    using FlightLogNet.Repositories.Interfaces;
    using Moq;
    using Xunit;

    public class GetExportToCsvOperationTests(GetExportToCsvOperation getExportToCsvOperation)
    {
        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var mockRepository = new Mock<IFlightRepository>();

            var towplane = new FlightModel
            {
                Id = 1,
                TakeoffTime = new DateTime(2024, 11, 18, 10, 0, 0),
                Airplane = new AirplaneModel { Id = 1, Immatriculation = "T123", Type = "Towplane" },
                Pilot = new PersonModel { MemberId = 1, FirstName = "John", LastName = "Doe" },
                Copilot = null,
                Task = "Towing"
            };

            var glider = new FlightModel
            {
                Id = 2,
                TakeoffTime = new DateTime(2024, 11, 18, 10, 10, 0),
                LandingTime = new DateTime(2024, 11, 18, 10, 40, 0),
                Airplane = new AirplaneModel { Id = 2, Immatriculation = "G456", Type = "Glider" },
                Pilot = new PersonModel { MemberId = 2, FirstName = "Jane", LastName = "Smith" },
                Copilot = new PersonModel { MemberId = 3, FirstName = "Alice", LastName = "Brown" },
                Task = "Training"
            };

            var reports = new List<ReportModel>
            {
                new ReportModel { Towplane = towplane, Glider = glider }
            };

            mockRepository.Setup(repo => repo.GetReport()).Returns(reports);

            var operation = new GetExportToCsvOperation(mockRepository.Object);

            var expectedCsv = new StringBuilder()
                .AppendLine(string.Join(";",
                    "FlightId",
                    "Datum",
                    "Type",
                    "Immatriculation",
                    "Pilot",
                    "Copilot",
                    "Task",
                    "TakeoffTime",
                    "LandingTime",
                    "Doba letu"))
                .AppendLine("1;18. 11. 2024;Towplane;T123;Doe, John;;Towing;10:00:00;;")
                .AppendLine("2;18. 11. 2024;Glider;G456;Smith, Jane;Brown, Alice;Training;10:10:00;10:40:00;00:30:00")
                .ToString();

            // Act
            var resultBytes = operation.Execute();
            var result = Encoding.UTF8.GetString(resultBytes);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCsv, result);
        }
    }
}

namespace FlightLogNet.Tests.Operation
{
    using FlightLogNet.Operation;

    using Xunit;

    public class GetExportToCsvOperationTests(GetExportToCsvOperation getExportToCsvOperation)
    {
        // TODO 6.1: Odstraòte skip a doplntì test, aby otestoval vrácený CSV soubor.
        [Fact(Skip = "Not implemented.")]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            // Act
            var result = getExportToCsvOperation.Execute();

            // Assert
            //Assert.Equal(expectedCsv, result);
        }
    }
}

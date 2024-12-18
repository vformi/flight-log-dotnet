namespace FlightLogNet.Tests.Operation
{
    using Integration;
    using Models;
    using FlightLogNet.Operation;
    using FlightLogNet.Repositories.Interfaces;

    using Moq;

    using Xunit;

    public class CreatePersonOperationTests
    {
        private readonly MockRepository mockRepository;

        private readonly Mock<IPersonRepository> mockPersonRepository;
        private readonly Mock<IClubUserDatabase> mockClubUserDatabase;

        public CreatePersonOperationTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockPersonRepository = this.mockRepository.Create<IPersonRepository>();
            this.mockClubUserDatabase = this.mockRepository.Create<IClubUserDatabase>();
        }

        private CreatePersonOperation CreateCreatePersonOperation()
        {
            return new CreatePersonOperation(
                this.mockPersonRepository.Object,
                this.mockClubUserDatabase.Object);
        }

        [Fact]
        public void Execute_ShouldReturnNull()
        {
            // Arrange
            var createPersonOperation = this.CreateCreatePersonOperation();

            // Act
            var result = createPersonOperation.Execute(null);

            // Assert
            Assert.Null(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Execute_ShouldCreateGuest()
        {
            // Arrange
            var createPersonOperation = this.CreateCreatePersonOperation();
            PersonModel personModel = new PersonModel
            {
                Address = new AddressModel { City = "NY", PostalCode = "456", Street = "2nd Ev", Country = "USA" },
                FirstName = "John",
                LastName = "Smith"
            };
            this.mockPersonRepository.Setup(repository => repository.AddGuestPerson(personModel)).Returns(10);

            // Act
            var result = createPersonOperation.Execute(personModel);

            // Assert
            Assert.True(result > 0);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Execute_ShouldCreateNewClubMember()
        {
            // Arrange
            var createPersonOperation = this.CreateCreatePersonOperation();
            
            PersonModel clubUser = new PersonModel
            {
                FirstName = "Karel",
                LastName = "Lucemburský",
                MemberId = 444
            };

            long id = 0; // Output ID placeholder

            // The person is not in the local repository
            this.mockPersonRepository.Setup(repository =>
                    repository.TryGetPerson(clubUser, out id))
                .Returns(false);

            // The person is found in the club database
            this.mockClubUserDatabase.Setup(repository =>
                    repository.TryGetClubUser(clubUser.MemberId, out clubUser))
                .Returns(true);

            // The person is saved in the local repository
            this.mockPersonRepository.Setup(repository =>
                    repository.CreateClubMember(clubUser))
                .Returns(4);

            // Act
            var result = createPersonOperation.Execute(clubUser);

            // Assert
            Assert.Equal(4, result); // Assert the correct ID is returned
            this.mockRepository.VerifyAll(); // Verify all mock setups were called
        }
    }
}

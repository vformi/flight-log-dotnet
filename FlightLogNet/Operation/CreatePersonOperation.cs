namespace FlightLogNet.Operation
{
    using System.Collections.Generic;

    using FlightLogNet.Integration;
    using FlightLogNet.Models;
    using FlightLogNet.Repositories.Interfaces;

    public class CreatePersonOperation
    {
        private const int GuestId = 0;
        private readonly IPersonRepository personRepository;
        private readonly IClubUserDatabase clubUserDatabase;

        public CreatePersonOperation(IPersonRepository personRepository,
            IClubUserDatabase clubUserDatabase)
        {
            this.personRepository = personRepository;
            this.clubUserDatabase = clubUserDatabase;
        }

        public long? Execute(PersonModel personModel)
        {
            if (personModel == null)
            {
                return null;
            }

            if (personModel.MemberId == GuestId)
            {
                return this.personRepository.AddGuestPerson(personModel);
            }

            if (this.personRepository.TryGetPerson(personModel, out long airplaneId))
            {
                return airplaneId;
            }

            if (this.clubUserDatabase.TryGetClubUser(personModel.MemberId, out PersonModel clubUser))
            {
                return this.personRepository.CreateClubMember(clubUser);
            }

            throw new KeyNotFoundException("Person is not guest and Person not found in internal Database.");
        }
    }
}

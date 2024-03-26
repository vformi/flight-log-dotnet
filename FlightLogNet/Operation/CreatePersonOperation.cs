namespace FlightLogNet.Operation
{
    using System.Collections.Generic;

    using Integration;
    using Models;
    using Repositories.Interfaces;

    public class CreatePersonOperation(
        IPersonRepository personRepository,
        IClubUserDatabase clubUserDatabase)
    {
        private const int GuestId = 0;

        public long? Execute(PersonModel personModel)
        {
            if (personModel == null)
            {
                return null;
            }

            if (personModel.MemberId == GuestId)
            {
                return personRepository.AddGuestPerson(personModel);
            }

            if (personRepository.TryGetPerson(personModel, out long airplaneId))
            {
                return airplaneId;
            }

            if (clubUserDatabase.TryGetClubUser(personModel.MemberId, out PersonModel clubUser))
            {
                return personRepository.CreateClubMember(clubUser);
            }

            throw new KeyNotFoundException("Person is not guest and Person not found in internal Database.");
        }
    }
}

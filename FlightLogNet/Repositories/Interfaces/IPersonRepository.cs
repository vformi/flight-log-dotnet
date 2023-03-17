namespace FlightLogNet.Repositories.Interfaces
{
    using FlightLogNet.Models;

    public interface IPersonRepository
    {
        long AddGuestPerson(PersonModel person);

        bool TryGetPerson(PersonModel personModel, out long personId);

        long CreateClubMember(PersonModel pilot);
    }
}
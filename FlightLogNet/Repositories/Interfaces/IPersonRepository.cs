namespace FlightLogNet.Repositories.Interfaces
{
    using Models;

    public interface IPersonRepository
    {
        long AddGuestPerson(PersonModel person);

        bool TryGetPerson(PersonModel personModel, out long personId);

        long CreateClubMember(PersonModel pilot);
    }
}

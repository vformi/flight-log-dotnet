namespace FlightLogNet.Integration
{
    using System.Collections.Generic;

    using Models;

    public interface IClubUserDatabase
    {
        IList<PersonModel> GetClubUsers();

        bool TryGetClubUser(long memberId, out PersonModel personModel);
    }
}

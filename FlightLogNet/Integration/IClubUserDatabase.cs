namespace FlightLogNet.Integration
{
    using System.Collections.Generic;

    using FlightLogNet.Models;

    public interface IClubUserDatabase
    {
        IList<PersonModel> GetClubUsers();

        bool TryGetClubUser(long memberId, out PersonModel personModel);
    }
}
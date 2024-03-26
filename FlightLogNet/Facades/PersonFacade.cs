namespace FlightLogNet.Facades
{
    using System.Collections.Generic;

    using Integration;
    using Models;

    public class PersonFacade(IClubUserDatabase clubUserDatabase)
    {
        internal IList<PersonModel> GetClubMembers()
        {
            return clubUserDatabase.GetClubUsers();
        }
    }
}

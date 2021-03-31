namespace FlightLogNet.Facades
{
    using System.Collections.Generic;

    using FlightLogNet.Integration;
    using FlightLogNet.Models;

    public class PersonFacade
    {
        private readonly IClubUserDatabase clubUserDatabase;

        public PersonFacade(IClubUserDatabase clubUserDatabase)
        {
            this.clubUserDatabase = clubUserDatabase;
        }

        internal IList<PersonModel> GetClubMembers()
        {
            return this.clubUserDatabase.GetClubUsers();
        }
    }
}

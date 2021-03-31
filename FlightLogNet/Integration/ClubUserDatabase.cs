namespace FlightLogNet.Integration
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using FlightLogNet.Models;

    using Microsoft.Extensions.Configuration;

    using RestSharp;

    public class ClubUserDatabase : IClubUserDatabase
    {
        // TODO 8.1: Přidejte si přes dependency injection configuraci 

        public bool TryGetClubUser(long memberId, out PersonModel personModel)
        {
            personModel = this.GetClubUsers().FirstOrDefault(person => person.MemberId == memberId);

            return personModel != null;
        }

        public IList<PersonModel> GetClubUsers()
        {
            IList<ClubUser> x = this.ReceiveClubUsers();
            return this.TransformToPersonModel(x);
        }

        private IList<ClubUser> ReceiveClubUsers()
        {
            // TODO 8.2: Naimplementujte volání endpointu ClubDB pomocí RestSharp

            return null;
        }

        private IList<PersonModel> TransformToPersonModel(IList<ClubUser> users)
        {
            return null;
        }
    }
}

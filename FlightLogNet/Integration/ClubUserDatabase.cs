namespace FlightLogNet.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Models;
    using Microsoft.Extensions.Configuration;
    using RestSharp;

    public class ClubUserDatabase : IClubUserDatabase
    {
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public ClubUserDatabase(IConfiguration configuration, IMapper mapper)
        {
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public bool TryGetClubUser(long memberId, out PersonModel personModel)
        {
            personModel = this.GetClubUsers().FirstOrDefault(person => person.MemberId == memberId);
            return personModel != null;
        }

        public IList<PersonModel> GetClubUsers()
        {
            IList<ClubUser> clubUsers = this.ReceiveClubUsers();
            return this.TransformToPersonModel(clubUsers);
        }

        private List<ClubUser> ReceiveClubUsers()
        {
            var client = new RestClient(this.configuration["ClubUsersApi"]);
            var request = new RestRequest("club/user", Method.Get);

            var response = client.Execute<List<ClubUser>>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Data != null)
            {
                return response.Data;
            }

            throw new Exception($"Failed to fetch club users. HTTP Status: {response.StatusCode}, Error: {response.ErrorMessage}");
        }

        private List<PersonModel> TransformToPersonModel(IList<ClubUser> users)
        {
            // automapper implementation
            return users.Select(user => this.mapper.Map<PersonModel>(user)).ToList();
        }
    }
}

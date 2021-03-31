namespace FlightLogNet.Integration
{
    using System.Collections.Generic;
    using System.Linq;

    using FlightLogNet.Models;

    public class ClubUserDatabaseStub : IClubUserDatabase
    {
        public IList<PersonModel> GetClubUsers()
        {
            List<ClubUser> x = ReceiveClubUsers();
            return TransformToPersonModel(x);
        }

        private static List<ClubUser> ReceiveClubUsers()
        {
            return new List<ClubUser>
            {
                new ClubUser { MemberId = 1L, FirstName = "Kamila", LastName = "Spoustová", Roles = new[] { "PILOT" } },
                new ClubUser { MemberId = 2L, FirstName = "Naděžda", LastName = "Pavelková", Roles = new[] { "PILOT" } },
                new ClubUser { MemberId = 3L, FirstName = "Silvie", LastName = "Hronová", Roles = new[] { "PILOT" } },
                new ClubUser { MemberId = 9L, FirstName = "Miloš", LastName = "Korbel", Roles = new[] { "PILOT", "BACKOFFICE" } },
                new ClubUser { MemberId = 10L, FirstName = "Petr", LastName = "Hrubec", Roles = new[] { "PILOT", "BACKOFFICE" } },
                new ClubUser { MemberId = 13L, FirstName = "Michal", LastName = "Vyvlečka", Roles = new[] { "BACKOFFICE" } }
            };
        }

        private static IList<PersonModel> TransformToPersonModel(List<ClubUser> x)
        {
            return x.Select(user => new PersonModel
            {
                MemberId = user.MemberId,
                FirstName = user.FirstName,
                LastName = user.LastName
            }).ToList();
        }

        public bool TryGetClubUser(long memberId, out PersonModel personModel)
        {
            personModel = this.GetClubUsers().FirstOrDefault(person => person.MemberId == memberId);

            return personModel != null;
        }
    }
}

namespace FlightLogNet.Repositories
{
    using System.Linq;

    using FlightLogNet.Models;
    using FlightLogNet.Repositories.Entities;
    using FlightLogNet.Repositories.Interfaces;

    public class PersonRepository : IPersonRepository
    {
        public long AddGuestPerson(PersonModel pilot)
        {
            using var dbContext = new LocalDatabaseContext();

            var address = new Address { City = pilot.Address.City, Country = pilot.Address.Country, PostalCode = pilot.Address.PostalCode, Street = pilot.Address.Street };
            var person = new Person { Address = address, FirstName = pilot.FirstName, LastName = pilot.LastName, PersonType = PersonType.Guest };

            dbContext.Persons.Add(person);
            dbContext.SaveChanges();

            return person.Id;
        }

        public long CreateClubMember(PersonModel pilot)
        {
            using var dbContext = new LocalDatabaseContext();

            var person = new Person
            {
                FirstName = pilot.FirstName,
                LastName = pilot.LastName,
                PersonType = PersonType.ClubMember,
                MemberId = pilot.MemberId,
            };

            dbContext.Persons.Add(person);
            dbContext.SaveChanges();

            return person.Id;
        }

        public bool TryGetPerson(PersonModel personModel, out long personId)
        {
            using var dbContext = new LocalDatabaseContext();

            Person firstPerson = dbContext.Persons.FirstOrDefault(person => person.MemberId == personModel.MemberId);
            if (firstPerson != null)
            {
                personId = firstPerson.Id;
                return true;
            }
            else
            {
                personId = 0;
                return false;
            }
        }
    }
}

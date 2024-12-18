namespace FlightLogNet.Models
{
    public class PersonModel
    {
        public long MemberId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Fullname => $"{LastName}, {FirstName}";

        public AddressModel Address { get; set; }
    }
}

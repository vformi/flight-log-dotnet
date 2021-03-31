namespace FlightLogNet.Repositories.Entities
{
    using System;

    using FlightLogNet.Models;

    public class Flight
    {
        public long Id { get; set; }

        public DateTime TakeoffTime { get; set; }

        public DateTime? LandingTime { get; set; }

        public Airplane Airplane { get; set; }

        public Person Pilot { get; set; }

        public Person Copilot { get; set; }

        public string Task { get; set; }

        public string Note { get; set; }

        public FlightType Type { get; set; }
    }
}

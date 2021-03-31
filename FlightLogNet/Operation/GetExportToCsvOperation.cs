namespace FlightLogNet.Operation
{
    using System.Collections.Generic;
    using System.Globalization;

    using FlightLogNet.Models;
    using FlightLogNet.Repositories.Interfaces;

    public class GetExportToCsvOperation
    {
        private readonly IFlightRepository flightRepository;

        public GetExportToCsvOperation(IFlightRepository flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        public byte[] Execute()
        {
            // TODO 5.1: Naimplementujte export do CSV
            // TIP: CSV soubor je pouze string, který se dá vytvořit pomocí třídy StringBuilder
            // TIP: Do bytové reprezentace je možné jej převést například pomocí metody: Encoding.UTF8.GetBytes(..)

            return new byte[0];
        }
    }
}

using System;
using System.Text;
using FlightLogNet.Models;

namespace FlightLogNet.Operation
{
    using Repositories.Interfaces;

    public class GetExportToCsvOperation(IFlightRepository flightRepository)
    {
        private const char Separator = ';';
        private readonly string header = string.Join(Separator,
            ReportLocalization.FlightIdColumn,
            ReportLocalization.DateColumn,
            ReportLocalization.TypeColumn,
            ReportLocalization.ImmatriculationColumn,
            ReportLocalization.PilotColumn,
            ReportLocalization.CopilotColumn,
            ReportLocalization.TaskColumn,
            ReportLocalization.TakeoffTimeColumn,
            ReportLocalization.LandingTimeColumn,
            ReportLocalization.FlightLengthColumn,
            Environment.NewLine
        );

        public byte[] Execute()
        {
            StringBuilder builder = new(header);

            var reports = flightRepository.GetReport();
            
            foreach (var report in reports)
            {
                AddRow(builder, report.Towplane);
                
                if (report.Glider != null)
                {
                    AddRow(builder, report.Glider);
                }
            }

            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        private static void AddRow(StringBuilder builder, FlightModel flight)
        {
            builder.Append($"{flight.Id}{Separator}");
            builder.Append($"{flight.TakeoffTime.ToString("dd. MM. yyyy")}{Separator}");
            builder.Append($"{flight.Airplane.Type}{Separator}");
            builder.Append($"{flight.Airplane.Immatriculation}{Separator}");
            builder.Append($"{flight.Pilot.LastName}, {flight.Pilot.FirstName}{Separator}");
            builder.Append($"{flight.Copilot?.LastName ?? string.Empty}{Separator}");
            builder.Append($"{flight.Task}{Separator}");
            builder.Append($"{flight.TakeoffTime.ToString("HH:mm:ss")}");

            if (flight.LandingTime != null)
            {
                builder.Append(Separator);
                builder.Append($"{flight.LandingTime.Value.ToString("HH:mm:ss")}{Separator}");
                builder.Append($"{flight.LandingTime - flight.TakeoffTime}");
            }

            builder.Append(Environment.NewLine);
        }
    }
}

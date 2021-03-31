namespace FlightLogNet
{
    using System;
    using System.Linq.Expressions;

    using AutoMapper;

    using FlightLogNet.Integration;
    using FlightLogNet.Models;
    using FlightLogNet.Repositories.Entities;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<Address, AddressModel>();
            this.CreateMapAirplanes();

            this.CreateMap<Flight, FlightModel>();
            this.CreateMap<FlightStart, ReportModel>();
            this.CreateMap<Person, PersonModel>();
            this.CreateMap<ClubUser, PersonModel>()
                .ForMember(dest => dest.Address, opt => opt.Ignore());
        }

        private void CreateMapAirplanes()
        {
            Expression<Func<Airplane, string>> immatriculationMap = airplane => airplane.ClubAirplane != null
                ? airplane.ClubAirplane.Immatriculation
                : airplane.GuestAirplaneImmatriculation;

            Expression<Func<Airplane, string>> typeMap = airplane => airplane.ClubAirplane != null
                ? airplane.ClubAirplane.AirplaneType.Type
                : airplane.GuestAirplaneType;

            this.CreateMap<Airplane, AirplaneModel>()
                .ForMember(dest => dest.Immatriculation, opt => opt.MapFrom(immatriculationMap))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(typeMap));

            this.CreateMap<ClubAirplane, AirplaneModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(airplane => airplane.AirplaneType.Type));
        }
    }
}

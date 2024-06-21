using AutoMapper;
using GraphQL.Address.Application.Dtos.Address;
using GraphQL.Address.Application.Dtos.Person;
using GraphQL.Address.Domain.Entities;

namespace GraphQL.Address.Application.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Person
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Person, CreatePersonEventDto>().ReverseMap();
            CreateMap<Person, UpdatePersonEventDto>().ReverseMap();

            //Address
            CreateMap<Domain.Entities.Address, AddressDto>().ReverseMap();
            CreateMap<Domain.Entities.Address, CreateAddressDto>().ReverseMap();
            CreateMap<Domain.Entities.Address, UpdateAddressDto>().ReverseMap();
        }
    }
}

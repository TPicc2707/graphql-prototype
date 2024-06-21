using AutoMapper;
using GraphQL.Person.Application.Dtos.Person;


namespace GraphQL.Person.Application.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Person
            CreateMap<Domain.Entities.Person, PersonDto>().ReverseMap();
            CreateMap<Domain.Entities.Person, CreatePersonDto>().ReverseMap();
            CreateMap<Domain.Entities.Person, UpdatePersonDto>().ReverseMap();
        }
    }
}

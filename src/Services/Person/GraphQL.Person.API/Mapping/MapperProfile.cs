using AutoMapper;
using GraphQL.EventBus.Messages.Events;
using GraphQL.Person.Application.Dtos.Person;

namespace GraphQL.Person.API.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PersonDto, CreatePersonEvent>().ReverseMap();
            CreateMap<UpdatePersonDto, UpdatePersonEvent>().ReverseMap();
            CreateMap<DeletePersonDto, DeletePersonEvent>().ReverseMap();
        }
    }
}

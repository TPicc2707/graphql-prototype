using AutoMapper;
using GraphQL.Address.Application.Dtos.Person;
using GraphQL.EventBus.Messages.Events;

namespace GraphQL.Address.API.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreatePersonEvent, CreatePersonEventDto>().ReverseMap();
            CreateMap<UpdatePersonEvent, UpdatePersonEventDto>().ReverseMap();
            CreateMap<DeletePersonEvent, DeletePersonEventDto>().ReverseMap();
        }
    }
}

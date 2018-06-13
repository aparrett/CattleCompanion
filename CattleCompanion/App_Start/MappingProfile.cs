using AutoMapper;
using CattleCompanion.Core.Dtos;
using CattleCompanion.Core.Models;

namespace CattleCompanion.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cow, CowDto>();
            CreateMap<CowDto, Cow>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            CreateMap<Relationship, RelationshipDto>();
            CreateMap<RelationshipDto, Relationship>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            CreateMap<CowEvent, CowEventDto>();
            CreateMap<CowEventDto, CowEvent>()
                .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}
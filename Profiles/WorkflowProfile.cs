using AutoMapper;
using CapitalApi.Dto;
using CapitalApi.Models;

namespace CapitalApi.Profiles
{
    public class WorkflowProfile : AutoMapper.Profile
    {
        public WorkflowProfile()
        {
            CreateMap<Workflow, WorkflowDTO>()
                .ForMember(dest => dest.ProgramId, opt => opt.MapFrom(src => src.ProgramId))
                .ForMember(dest => dest.Stage, opt => opt.MapFrom(src => src.Stage))
                .ForMember(dest => dest.Applications, opt => opt.MapFrom(src => src.Applications));

            CreateMap<ProgramApplication, ProgramApplicationDto>()
                .ForMember(dest => dest.ApplicationId, opt => opt.MapFrom(src => src.ApplicationId))
                .ForMember(dest => dest.ApplicantName, opt => opt.MapFrom(src => src.ApplicantName))
                .ForMember(dest => dest.ApplicationDate, opt => opt.MapFrom(src => src.ApplicationDate));
        }
    }
}

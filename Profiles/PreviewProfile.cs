using AutoMapper;
using CapitalApi.Dto;
using CapitalApi.Models;
using System.Linq;

namespace CapitalApi.Profiles
{
    public class PreviewProfile : AutoMapper.Profile
    {
        public PreviewProfile()
        {
            CreateMap<Preview, PreviewDTO>()
                .ForMember(dest => dest.Program, opt => opt.MapFrom(src => src.Program))
                .ForMember(dest => dest.Templates, opt => opt.MapFrom(src => src.Templates))
                .ForMember(dest => dest.Workflow, opt => opt.MapFrom(src => src.Workflow));

            CreateMap<ProgramModel, ProgramDTO>();
            CreateMap<ProgramAdditionalInfo, ProgramAdditionalInfoDTO>();

            CreateMap<Template, TemplateDTO>();
            CreateMap<Workflow, WorkflowDTO>();
        }
    }
}

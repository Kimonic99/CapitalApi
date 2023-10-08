using AutoMapper;
using CapitalApi.Dto;
using CapitalApi.Models;

namespace CapitalApi.Profiles
{
    public class TemplateProfile : AutoMapper.Profile
    {
        public TemplateProfile()
        {
            CreateMap<TemplateDTO, Template>()
                .ForMember(dest => dest.ProgramId, opt => opt.MapFrom(src => src.ProgramId))
                .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom(src => src.CoverImageBase64))
                .ForMember(dest => dest.PersonalInfo, opt => opt.MapFrom(src => src.PersonalInfo))
                .ForMember(dest => dest.Profile, opt => opt.MapFrom(src => src.Profile))
                .ForMember(dest => dest.AdditionalQuestions, opt => opt.MapFrom(src => src.AdditionalQuestions));

            CreateMap<PersonalInfoDTO, PersonalInfo>();
            CreateMap<ProfileDTO, Models.Profile>()
                .ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.Education))
                .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience))
                .ForMember(dest => dest.ResumeUrl, opt => opt.MapFrom(src => src.ResumeBase64));
            
            CreateMap<AdditionalQuestionDTO, AdditionalQuestion>()
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options));
        }
    }
}

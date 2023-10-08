using CapitalApi.Models;
using CapitalApi.Dto;
using AutoMapper;


namespace CapitalApi.Profiles
{
    public class ProgramProfile : AutoMapper.Profile // Use AutoMapper's Profile
    {
        public ProgramProfile()
        {
            CreateMap<ProgramDTO, ProgramModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProgramTitle, opt => opt.MapFrom(src => src.ProgramTitle))
                .ForMember(dest => dest.ProgramDescription, opt => opt.MapFrom(src => src.ProgramDescription))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.SkillsRequired, opt => opt.MapFrom(src => src.SkillsRequired))
                .ForMember(dest => dest.ProgramBenefits, opt => opt.MapFrom(src => src.ProgramBenefits))
                .ForMember(dest => dest.ApplicationCriteria, opt => opt.MapFrom(src => src.ApplicationCriteria))
                .ForMember(dest => dest.AdditionalInfo, opt => opt.MapFrom(src => src.AdditionalInfo));


            CreateMap<ProgramAdditionalInfoDTO, ProgramAdditionalInfoModel>()
                .ForMember(dest => dest.ProgramType, opt => opt.MapFrom(src => src.ProgramType))
                .ForMember(dest => dest.ProgramStart, opt => opt.MapFrom(src => src.ProgramStart))
                .ForMember(dest => dest.ApplicationOpen, opt => opt.MapFrom(src => src.ApplicationOpen))
                .ForMember(dest => dest.ApplicationClose, opt => opt.MapFrom(src => src.ApplicationClose))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.ProgramLocation, opt => opt.MapFrom(src => src.ProgramLocation))
                .ForMember(dest => dest.MinQualifications, opt => opt.MapFrom(src => src.MinQualifications))
                .ForMember(dest => dest.MaxNumberOfApplications, opt => opt.MapFrom(src => src.MaxNumberOfApplications));

            CreateMap<ProgramAdditionalInfoModel, ProgramAdditionalInfoDTO>();

        }
    }
}

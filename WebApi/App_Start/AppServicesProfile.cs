using AutoMapper;
using BusinessLayer.Model.Models;
using WebApi.Models;

namespace WebApi
{
    public class AppServicesProfile : Profile
    {
        public AppServicesProfile()
        {
            CreateMapper();
        }

        private void CreateMapper()
        {
            CreateMap<BaseInfo, BaseDto>();
            
            CreateMap<EmployeeInfo, EmployeeDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyCode))
                .ForMember(dest => dest.OccupationName, opt => opt.MapFrom(src => src.Occupation))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.LastModifiedDateTime, opt => opt.MapFrom(src => src.LastModified.ToString("yyyy-MM-dd HH:mm:ss")));
            
            CreateMap<EmployeeDto, EmployeeInfo>()
                .ForMember(e => e.CompanyCode, (opt) => opt.MapFrom(e => e.CompanyName))
                .ForMember(e => e.Occupation, (opt) => opt.MapFrom(e => e.OccupationName))
                .ForMember(e => e.Phone, (opt) => opt.MapFrom(e => e.PhoneNumber))
                .ForMember(e => e.LastModified, (opt) => opt.MapFrom(e => e.LastModifiedDateTime));
            CreateMap<CompanyInfo, CompanyDto>();
            CreateMap<ArSubledgerInfo, ArSubledgerDto>();
        }
    }
}
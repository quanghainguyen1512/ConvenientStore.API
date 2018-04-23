using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConvenientShop.API.Helpers
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<Entities.Staff, Models.StaffDto>()
                .ForMember(dest => dest.StaffName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => Helpers.DateOfBirthToAge(src.DateOfBirth)))
                .ForMember(dept => dept.Gender,
                    opt => opt.MapFrom(src => src.Gender == true ? "Male" : "Female"));
            CreateMap<Entities.Product, Models.ProductDto>()
                .ForMember(dest => dest.SupplierName,
                    opt => opt.MapFrom(src => src.Supplier.SupplierName))
                .ForMember(dest => dest.Category,
                    opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}

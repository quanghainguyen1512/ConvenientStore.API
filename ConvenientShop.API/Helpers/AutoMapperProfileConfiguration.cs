using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace ConvenientShop.API.Helpers
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            // Staff
            CreateMap<Entities.Staff, Models.StaffDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => Helpers.DateOfBirthToAge(src.DateOfBirth)))
                .ForMember(dept => dept.Gender,
                    opt => opt.MapFrom(src => src.Gender == true ? "Nam" : "Nữ"));
            CreateMap<Entities.Staff, Models.StaffSimpleDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => Helpers.DateOfBirthToAge(src.DateOfBirth)))
                .ForMember(dept => dept.Gender,
                    opt => opt.MapFrom(src => src.Gender == true ? "Nam" : "Nữ"));

            //Product
            CreateMap<Entities.Product, Models.ProductDto>()
                .ForMember(dest => dest.SupplierName,
                    opt => opt.MapFrom(src => src.Supplier.SupplierName))
                .ForMember(dest => dest.Category,
                    opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Entities.Product, Models.ProductWithoutDetailDto>();

            //Supplier
            CreateMap<Entities.Supplier, Models.SupplierDto>();
            CreateMap<Entities.Supplier, Models.SupplierWithoutProductsDto>();
            CreateMap<Models.SupplierWithoutProductsDto, Entities.Supplier>();

            //Customer
            CreateMap<Entities.Customer, Models.CustomerDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => Helpers.DateOfBirthToAge(src.DateOfBirth)))
                .ForMember(dest => dest.Gender,
                    opt => opt.MapFrom(src => src.Gender == true ? "Male" : "Female"))
                .ForMember(dest => dest.CustomerType,
                    opt => opt.MapFrom(src => src.CustomerType.Name));
            CreateMap<Entities.Customer, Models.CustomerSimpleDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => Helpers.DateOfBirthToAge(src.DateOfBirth)));

            CreateMap<Models.CustomerForOperationsDto, Entities.Customer>();

            //Bill
            CreateMap<Entities.Bill, Models.BillDto>()
                .ForMember(dest => dest.CustomerName,
                    opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"))
                .ForMember(dest => dest.StaffName,
                    opt => opt.MapFrom(src => $"{src.Staff.FirstName} {src.Staff.LastName}"));
            CreateMap<Models.BillForOperationsDto, Entities.Bill>()
                .ForMember(dest => dest.Customer.CustomerId,
                    opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.Staff.StaffId,
                    opt => opt.MapFrom(src => src.StaffId));
            CreateMap<Entities.Bill, Models.BillForStaffDto>()
                .ForMember(dest => dest.StaffName,
                    opt => opt.MapFrom(src => $"{src.Staff.FirstName} {src.Staff.LastName}"));
        }
    }
}
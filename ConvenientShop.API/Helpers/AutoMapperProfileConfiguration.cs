﻿using System;
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
            CreateMap<Entities.Staff, Models.StaffSimpleDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => Helpers.DateOfBirthToAge(src.DateOfBirth)))
                .ForMember(dept => dept.Gender,
                    opt => opt.MapFrom(src => src.Gender == true ? "Nam" : "Nữ"))
                .Include<Entities.Staff, Models.StaffDto>();
            CreateMap<Entities.Staff, Models.StaffDto>();
            CreateMap<Entities.Staff, Models.StaffLogInSessionInfo>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            // .ForMember(dest => dest.FullName,
            //     opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            // .ForMember(dest => dest.Age,
            //     opt => opt.MapFrom(src => Helpers.DateOfBirthToAge(src.DateOfBirth)))
            // .ForMember(dept => dept.Gender,
            //     opt => opt.MapFrom(src => src.Gender == true ? "Nam" : "Nữ"));

            //Product
            CreateMap<Entities.Product, Models.ProductWithoutDetailDto>()
                .ForMember(dest => dest.SupplierName,
                    opt => opt.MapFrom(src => src.Supplier.SupplierName))
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name))
                .Include<Entities.Product, Models.ProductDto>();
            CreateMap<Entities.Product, Models.ProductDto>();

            //ProductDetail
            CreateMap<Entities.ProductDetail, Models.ProductDetailDto>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<Models.ProductDetailForOperationsDto, Entities.ProductDetail>();

            //Supplier
            CreateMap<Entities.Supplier, Models.SupplierWithoutProductsDto>()
                .Include<Entities.Supplier, Models.SupplierDto>();
            CreateMap<Models.SupplierWithoutProductsDto, Entities.Supplier>();

            //Customer
            CreateMap<Entities.Customer, Models.CustomerSimpleDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => Helpers.DateOfBirthToAge(src.DateOfBirth)))
                .Include<Entities.Customer, Models.CustomerDto>();
            CreateMap<Entities.Customer, Models.CustomerDto>()
                // .ForMember(dest => dest.FullName,
                //     opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                // .ForMember(dest => dest.Age,
                //     opt => opt.MapFrom(src => Helpers.DateOfBirthToAge(src.DateOfBirth)))
                .ForMember(dest => dest.Gender,
                    opt => opt.MapFrom(src => src.Gender == true ? "Male" : "Female"))
                .ForMember(dest => dest.CustomerType,
                    opt => opt.MapFrom(src => src.CustomerType.Name));
            CreateMap<Models.CustomerForOperationsDto, Entities.Customer>();

            //Bill
            CreateMap<Entities.Bill, Models.BillDto>()
                // .ForMember(dest => dest.CustomerName,
                //     opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"))
                .ForMember(dest => dest.StaffName,
                    opt => opt.MapFrom(src => $"{src.Staff.FirstName} {src.Staff.LastName}"));
            CreateMap<Models.BillForOperationsDto, Entities.Bill>();
            // .ForPath(dest => dest.Customer.CustomerId,
            // opt => opt.MapFrom(src => src.CustomerId))
            // .ForPath(dest => dest.Staff.StaffId,
            //     opt => opt.MapFrom(src => src.StaffId));

            CreateMap<Entities.Bill, Models.BillSimpleDto>()
                .ForMember(dest => dest.StaffName,
                    opt => opt.MapFrom(src => $"{src.Staff.FirstName} {src.Staff.LastName}"));

            //Order
            CreateMap<Entities.Order, Models.OrderDto>()
                .ForMember(dest => dest.StaffName,
                    opt => opt.MapFrom(src => $"{src.Staff.FirstName} {src.Staff.LastName}"));
            CreateMap<Models.OrderForOperationsDto, Entities.Order>();

            //Delivery
            CreateMap<Entities.Delivery, Models.DeliveryDto>();
            CreateMap<Models.DeliveryForOperationsDto, Entities.Delivery>()
                .ForPath(dest => dest.Supplier.SupplierId,
                    opt => opt.MapFrom(src => src.SupplierId));
        }
    }
}
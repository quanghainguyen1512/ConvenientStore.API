using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ConvenientShop.API.Helpers;
using ConvenientShop.API.Models;
using ConvenientShop.API.Services;
using ConvenientShop.API.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConvenientShop.API
{
    public class Startup
    {
        private readonly MapperConfiguration _mapperConfiguration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(obj => obj.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));

            services.Configure<StoreConfig>(Configuration.GetSection("ApiConfig"));
            
            services.AddScoped<IConvenientStoreRepository, ConvenientStoreRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            //AutoMapper.Mapper.Initialize(config =>
            //{
            //    config.CreateMap<Entities.Staff, Models.StaffDto>()
            //        .ForMember(dest => dest.StaffName,
            //            opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            //        .ForMember(dest => dest.Age,
            //            opt => opt.MapFrom(src => Helpers.Helpers.DateOfBirthToAge(src.DateOfBirth)))
            //        .ForMember(dept => dept.Gender,
            //            opt => opt.MapFrom(src => src.Gender == true ? "Male" : "Female"));
            //    config.CreateMap<Entities.Product, Models.ProductDto>()
            //        .ForMember(dest => dest.SupplierName,
            //            opt => opt.MapFrom(src => src.Supplier.SupplierName))
            //        .ForMember(dest => dest.Category,
            //            opt => opt.MapFrom(src => src.Category.Name));
            //});

        }
    }
}

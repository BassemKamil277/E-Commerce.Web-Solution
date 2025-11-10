using AutoMapper;
using E_Commerce.Domain.Entites.ProductModule;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.MappingProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandDTO>();

            CreateMap<Product, ProductDTO>()
                .ForMember(x => x.ProductBrand, opt => opt.MapFrom(x => x.ProductBrand.Name))
                .ForMember(x => x.ProductType, opt => opt.MapFrom(x => x.ProductType.Name))
                .ForMember(x => x.PictureUrl, opt => opt.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductType, TypeDTO>();


        }
    }
}

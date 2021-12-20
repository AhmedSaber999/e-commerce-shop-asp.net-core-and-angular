using API.DataShape;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductReturnedData>()
            .ForMember(destinationMember => destinationMember.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(destinationMember => destinationMember.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(destinationMember => destinationMember.PictureUrl, o => o.MapFrom<ProductUrlResolver>());


            CreateMap<Address, AddressDto>().ReverseMap();
        }   
    }
}
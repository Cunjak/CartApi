using AutoMapper;
using Business.HelperModels;
using Data.Models;

namespace Business.Helper
{
    public class ApplicationMapper: Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Data.Models.Cart, CartOverview>();

            CreateMap<CartItem, CartItemBasicAttributes>();

            CreateMap<CartItemFromBody, CartItem>();
            
        }
    }
}

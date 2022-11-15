using ApplicationCore.Models2;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Category, DTOs.Category>().ReverseMap();
            CreateMap<Account, DTOs.Account>().ReverseMap();
            CreateMap<Order, DTOs.Order>().ReverseMap();
            CreateMap<OrderDetail, DTOs.OrderDetail>().ReverseMap();
            CreateMap<Product, DTOs.Product>().ReverseMap();
            CreateMap<Supplier, DTOs.Supplier>().ReverseMap();
        }
    }
}

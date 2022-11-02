using ApplicationCore.Models2;
using AutoMapper;
using BusinessService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Service
{
    public class ProductService : BaseService<Product, DTOs.Product>, IProductService
    {
        public ProductService(PizzaStoreContext context, IMapper mapper) : base(context, mapper)
        {

        }
        public override void DisableSelfReference(ref Product entity)
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            if (entity.Supplier != null)
            {
                entity.Supplier.Products = null;
            }
            entity.OrderDetails = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }
    }
}

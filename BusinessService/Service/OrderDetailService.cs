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
    public class OrderDetailService : BaseService<OrderDetail, DTOs.OrderDetail>, IOrderDetailService
    {
        private IProductService _productService;
        public OrderDetailService(PizzaStoreContext context, IMapper mapper) : base(context, mapper)
        {
            _productService = new ProductService(context, mapper);
        }
        public override void DisableSelfReference(ref OrderDetail entity)
        {
            if(entity.Order != null)
                entity.Order.OrderDetails = null;
            if(entity.Product != null)
            {
                var product = entity.Product;
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                product.OrderDetails = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                _productService.DisableSelfReference(ref product);
                entity.Product = product;
            }
        }
    }
}

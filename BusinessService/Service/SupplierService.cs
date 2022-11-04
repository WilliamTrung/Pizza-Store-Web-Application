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
    public class SupplierService : BaseService<Supplier, DTOs.Supplier>, ISupplierService
    {
        private IProductService _productService;
        public SupplierService(PizzaStoreContext context, IMapper mapper) : base(context, mapper)
        {
            _productService = new ProductService(context, mapper);
        }
        public override void DisableSelfReference(ref Supplier entity)
        {
            if(entity.Products != null)
            {
                var products = new List<Product>();
                for(int i = 0; i < entity.Products.Count; i++)
                {
                    var product = entity.Products.ElementAt(i);
                    product.Supplier = null;
                    _productService.DisableSelfReference(ref product);
                    products.Add(product);
                }
                entity.Products = products;
            }
        }
    }
}

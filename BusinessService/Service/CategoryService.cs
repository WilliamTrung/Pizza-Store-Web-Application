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
    public class CategoryService : BaseService<Category, DTOs.Category>, ICategoryService
    {
        private IProductService _productService;
        public CategoryService(PizzaStoreContext context, IMapper mapper) : base(context, mapper)
        {
            _productService = new ProductService(context, mapper);
        }
        public override void DisableSelfReference(ref Category entity)
        {
            if (entity.Products != null)
            {
                var products = new List<Product>();
                for (int i = 0; i < entity.Products.Count; i++)
                {
                    var product = entity.Products.ElementAt(i);
                    product.Category = null;
                    _productService.DisableSelfReference(ref product);
                    products.Add(product);
                }
                entity.Products = products;
            }
        }
    }
}

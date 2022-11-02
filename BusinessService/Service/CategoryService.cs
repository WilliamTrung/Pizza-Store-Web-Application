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
        public CategoryService(PizzaStoreContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public override void DisableSelfReference(ref Category entity)
        {
            if(entity.Products != null)
            {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                entity.Products = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            }
        }
    }
}

using ApplicationCore.Models2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.IService
{
    public interface IProductService : IBaseService<Product, DTOs.Product>
    {
    }
}

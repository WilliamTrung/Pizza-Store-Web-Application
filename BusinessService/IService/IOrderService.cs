﻿using ApplicationCore.Models2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.IService
{
    public interface IOrderService : IBaseService<Order, DTOs.Order>
    {
        Task<DTOs.Order> CreateOrder(Cart cart);
    }
}

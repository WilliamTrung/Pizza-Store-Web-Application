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
    public class OrderService : BaseService<Order, DTOs.Order>, IOrderService
    {
        IOrderDetailService _orderDetailService;
        public OrderService(PizzaStoreContext context, IMapper mapper, IOrderDetailService orderDetailService) : base(context, mapper)
        {
            _orderDetailService = orderDetailService;
        }


        public async Task<bool> CreateOrder(Cart cart)
        {
            if(cart == null || cart.account == null || cart.products == null || cart.products.Count <= 0)
                return false;
            //create order
            DTOs.Order order = new DTOs.Order();
            order.OrderDate = DateTime.Now;
            order.ShipAddress = cart.account.Address;
            //order.Customer =
            if (await Create(order))
            {

            }
            return false;
        }
    }
}

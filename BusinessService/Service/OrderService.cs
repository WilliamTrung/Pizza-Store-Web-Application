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


        public async Task<DTOs.Order> CreateOrder(Cart cart)
        {
            if(cart == null || cart.Account == null || cart.Products == null || cart.Products.Count <= 0)
                return null;
            //create order
            DTOs.Order order = new DTOs.Order();
            order.OrderDate = DateTime.Now;
            order.ShipAddress = cart.Account.Address;
            order.Account = cart.Account;
            order.RequiredDate = DateTime.Now.AddDays(3);
            //order.Customer =
            var created_order_entity = await Create(order);
            if(created_order_entity != null)
            {
                //created
                var created_order = _mapper.Map<DTOs.Order>(created_order_entity);
                List<DTOs.OrderDetail> details = new List<DTOs.OrderDetail>();
                foreach (var product in cart.Products)
                {
                    var detail = new DTOs.OrderDetail();
                    detail.OrderId = created_order.OrderId;
                    detail.ProductId = product.ProductId;
                    detail.UnitPrice = product.UnitPrice;
                    detail.Quantity = product.QuantityPerUnit;
                    
                    var detail_entity = await _orderDetailService.Create(detail);
                    if(detail_entity != null)
                    {
                        detail = _mapper.Map<DTOs.OrderDetail>(detail_entity);
                        details.Add(detail);
                    }
                }
                order.OrderDetails = details;
                return order;
            } else
            {
                return null;
            }
        }
    }
}

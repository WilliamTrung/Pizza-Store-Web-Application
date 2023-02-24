using ApplicationCore.Models2;
using AutoMapper;
using BusinessService.IService;
using BusinessService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAccountService AccountService { get; private set; } = null!;

        public ICategoryService CategoryService { get; private set; } = null!;

        public IOrderDetailService OrderDetailService { get; private set; } = null!;

        public IOrderService OrderService { get; private set; } = null!;

        public IProductService ProductService { get; private set; } = null!;

        public ISupplierService SupplierService { get; private set; } = null!;

        private readonly PizzaStoreContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(PizzaStoreContext context,  IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            InitRepositories();
        }

        private void InitRepositories()
        {
            AccountService = new AccountService(_context, _mapper);
            CategoryService = new CategoryService(_context, _mapper);
            OrderDetailService = new OrderDetailService(_context, _mapper);
            OrderService = new OrderService(_context, _mapper, OrderDetailService);
            ProductService = new ProductService(_context, _mapper);
            SupplierService = new SupplierService(_context, _mapper);
        }
    }
}

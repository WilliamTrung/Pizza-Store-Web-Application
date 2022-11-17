using ApplicationCore.Models2;
using ApplicationCore.Repository;
using AutoMapper;
using BusinessService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Service
{
    //BaseService<Category, DTOs.Category>, ICategoryService
    public class AccountService : BaseService<Account, DTOs.Account>, IAccountService
    {
        public AccountService(PizzaStoreContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<DTOs.Account?> Login(DTOs.Account account)
        {
            //throw new NotImplementedException();
            var find = await GetDTOs(filter: a => a.Username == account.Username && a.Password == account.Password);
            var found = find.FirstOrDefault();
            if(found != null)
            {
                return found;
            }
            return null;
        }

        public async Task<DTOs.Account?> Register(DTOs.Account account)
        {
            //throw new NotImplementedException();
            var find = await GetDTOs(filter: a => a.Username == account.Username && a.Password == account.Password);
            var found = find.FirstOrDefault();
            if (found == null)
            {
                //set type to customer
                account.Type = 1;
                var entity = await Create(account);
                if(entity != null)
                {
                    return _mapper.Map<DTOs.Account>(entity);
                }
            }
            return null;
        }
        public override Task<DTOs.Account> Create(DTOs.Account dto)
        {
            //check duplicate email
            if (dto == null)
                return null;
            return base.Create(dto);
        }
    }
}

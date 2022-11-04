using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using BusinessService.IService;
using BusinessService.Service;
using PizzaStoreApp.Helper;
using BusinessService.DTOs;
using Microsoft.Extensions.Primitives;

namespace PizzaStoreApp.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class Authorized : Attribute, IAsyncPageFilter
    {
        IAccountService? _accountService;
        string? _roles = null;
        public Authorized()
        {

        }
        public Authorized(string role)
        {
            _roles = role;
        }
        private void SetService(FilterContext context)
        {
            var services = context.HttpContext.RequestServices;
            _accountService = services.GetRequiredService<IAccountService>();
        }
        private bool Authorizing(BusinessService.DTOs.Account? login_user, string[] roles)
        {
            bool isAuthorized = false;
            if(login_user == null && roles.Length == 0)
            {
                //allow anonymous
                isAuthorized = true;
            } else if(login_user != null && roles.Length > 0)
            {
                //check login user with passed roles
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                if (login_user.Type != null)
                {
                    if (roles.Any(r => r == login_user.Type.ToString()))
                    {
                        //is valid
                        isAuthorized = true;
                    }
                }
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'

            } 
            return isAuthorized;
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            if(_roles == null)
            {
                return Task.CompletedTask;
            }
            var login = context.HttpContext.Session.GetLoginUser();
            var roles = _roles.Split(",");
            var flag = Authorizing(login, roles);
            if (flag)
                return Task.CompletedTask;
            return Task.FromException(new Exception(StatusCodes.Status401Unauthorized.ToString()));
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            //throw new NotImplementedException();
            await next.Invoke();
        }
    }
}

using BusinessService;
using BusinessService.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaStoreApp.Helper;

namespace PizzaStoreApp.Pages.Customer
{
    public class CartModel : PageModel
    {
        IOrderService _orderService;
        public CartModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public string ErrorMessage = "";
        [BindProperty]
        public Cart Cart { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Cart = SessionExtension.GetCart(HttpContext.Session);
            return Page();

        }
        public async Task<IActionResult> OnPost(int? remove, int? update, int? quantity)
        {
            if(remove == null && update != null)
            {
                //update
                var cart = SessionExtension.GetCart(HttpContext.Session);
                //adjust quantity
                var find = cart.Products.FirstOrDefault(p => p.ProductId == update);
                if(find != null)
                {
                    find.QuantityPerUnit = (int)quantity;
                }
                SessionExtension.Set<Cart>(HttpContext.Session, "cart", cart);
            } else if(update == null && remove != null)
            {
                var cart = SessionExtension.GetCart(HttpContext.Session);
                var products = cart.Products.Where(p => p.ProductId == remove);
                var product = products.FirstOrDefault();
                cart.Products.Remove(product);
                SessionExtension.Set<Cart>(HttpContext.Session, "cart", cart);
            } else
            {
                //check out
                var cart = SessionExtension.GetCart(HttpContext.Session);
                if(cart != null && cart.Account != null && cart.Products != null && cart.Products.Count() > 0)
                {
                    var order = await _orderService.CreateOrder(cart);
                    cart = new Cart();
                    SessionExtension.Set<Cart>(HttpContext.Session, "cart", cart);
                    return RedirectToPage("/Customer/Orders/Index");
                }
                ErrorMessage = "Cart is empty!";
            }
            return await OnGet();
        }

    }
}

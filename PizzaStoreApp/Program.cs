using ApplicationCore.Models2;
using BusinessService;
using BusinessService.DTOs;
using BusinessService.IService;
using BusinessService.Service;
using Microsoft.EntityFrameworkCore;

namespace PizzaStoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Init values from appsettings.json
            string connection;
            using (var config = builder.Configuration)
            {
                connection = config.GetConnectionString("DefaultConnection");
            }
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30));
            builder.Services.AddAutoMapper(typeof(Mapping));
            builder.Services.AddDbContext<PizzaStoreContext>(options => options.UseSqlServer(connection));
            // Add Transient 
            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<IOrderDetailService, OrderDetailService>();
            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<ISupplierService, SupplierService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            CreateDB(app);

            app.Run();
        }

        private static void CreateDB(WebApplication host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<PizzaStoreContext>();
                    context.Database.EnsureCreated();
                } catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured creating DB!");
                }
            }
        }
    }
}


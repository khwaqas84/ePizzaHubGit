using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Repositories.Implementations;
using ePizzaHub.Repositories.Interfaces;
using ePizzaHub.Services.Implementation;
using ePizzaHub.Services.Implementations;
using ePizzaHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ePizzaHub.Services
{
    public class ConfigureDependencies
    {

        public static void RegisterDependencies(IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            //Register repositories
            service.AddScoped<IRepository<Item>, Repository<Item>>();
            service.AddScoped<IRepository<CartItem>, Repository<CartItem>>();
            service.AddScoped<IUserRepository,UserRepository>();
            service.AddScoped<ICartRepository, CartRepository>();
            service.AddScoped<IRepository<PaymentDetail>, Repository<PaymentDetail>>();
            service.AddScoped<IOrderRepository, OrderRepository>();

            //Register Services
            service.AddScoped<IAuthService,AuthService>();
            service.AddScoped<ICartService, CartService>();
            service.AddScoped<IItemService, ItemService>();
            service.AddScoped<IPaymentService, PaymentService>();
            service.AddScoped<IOrderService, OrderService>();
        }
    }
}

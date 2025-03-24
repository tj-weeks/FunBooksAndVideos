using FunBooksAndVideos.Entities;
using FunBooksAndVideos.Events;
using FunBooksAndVideos.OrderProcessing;
using FunBooksAndVideos.PurchaseOrder;

namespace FunBooksAndVideos
{
    public static class Program
    {
        [Obsolete]
        public async static Task<int> Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<PurchaseOrderCommandHandler>());

            builder.Services.AddScoped<ICustomer, Customer>();
            builder.Services.AddScoped<ShippingSlipGenerator>();
            builder.Services.AddScoped<ItemProcessorFactory>();
            builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();

            // Register EventBus and Handlers
            builder.Services.AddSingleton<IEventBus, EventBus>();
            builder.Services.AddScoped<ActivateMembershipEventHandler>();
            builder.Services.AddScoped<GenerateShippingSlipEventHandler>();

            var app = builder.Build();

            // Subscribe to events
            using (var scope = app.Services.CreateScope())
            {
                var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
                var activateMembershipEventHandler = scope.ServiceProvider.GetRequiredService<ActivateMembershipEventHandler>();
                eventBus.Subscribe<ActivateMembershipEvent>(activateMembershipEventHandler.Handle);
                var generateShippingSlipEventHandler = scope.ServiceProvider.GetRequiredService<GenerateShippingSlipEventHandler>();
                eventBus.Subscribe<GenerateShippingSlipEvent>(generateShippingSlipEventHandler.Handle);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            return 0;
        }
    }
}

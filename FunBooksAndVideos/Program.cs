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
            builder.Services.AddScoped<ActivateMembershipEventHandler>();
            builder.Services.AddScoped<ShippingSlipGenerator>();
            builder.Services.AddScoped<GenerateShippingSlipEventHandler>();
            builder.Services.AddScoped<ItemProcessorFactory>();
            builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();

            var app = builder.Build();

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

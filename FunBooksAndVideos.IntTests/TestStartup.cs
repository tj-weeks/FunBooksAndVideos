using FunBooksAndVideos.Events;
using FunBooksAndVideos.OrderProcessing;
using FunBooksAndVideos.PurchaseOrder;
using FunBooksAndVideos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FunBooksAndVideos.IntTests
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new PurchaseItemConverter());
                });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<PurchaseOrderCommandHandler>());
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IShippingSlipService, ShippingSlipService>();
            services.AddScoped<ItemProcessorFactory>();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();

            // Register EventBus and Handlers
            services.AddSingleton<IEventBus, EventBus>();
            services.AddScoped<ActivateMembershipEventHandler>();
            services.AddScoped<GenerateShippingSlipEventHandler>();
        }

        public void Configure(IApplicationBuilder app)
        {
            // Configure the HTTP request pipeline.
            //if (app.ApplicationServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Subscribe to events
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
                var activateMembershipEventHandler = scope.ServiceProvider.GetRequiredService<ActivateMembershipEventHandler>();
                eventBus.Subscribe<ActivateMembershipEvent>(activateMembershipEventHandler.Handle);
                var generateShippingSlipEventHandler = scope.ServiceProvider.GetRequiredService<GenerateShippingSlipEventHandler>();
                eventBus.Subscribe<GenerateShippingSlipEvent>(generateShippingSlipEventHandler.Handle);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var projectDir = Directory.GetCurrentDirectory();
                    var launchSettingsPath = Path.Combine(projectDir, "Properties", "launchSettings.json");

                    config.AddJsonFile(launchSettingsPath, optional: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TestStartup>();
                });
    }
}

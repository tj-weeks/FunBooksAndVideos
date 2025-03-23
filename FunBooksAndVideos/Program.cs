using FunBooksAndVideos.Entities;
using FunBooksAndVideos.Events;
using FunBooksAndVideos.OrderProcessing;
using Endpoint = NServiceBus.Endpoint;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure NServiceBus
var endpointConfiguration = new EndpointConfiguration("FunBooksAndVideos");
var transport = endpointConfiguration.UseTransport<LearningTransport>();
endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>(); // Configure JSON serializer
endpointConfiguration.Conventions().DefiningEventsAs(type => type.Namespace == "FunBooksAndVideos.Events");

// Use the built-in dependency injection container
endpointConfiguration.RegisterComponents(services =>
{
    services.AddSingleton(builder.Services);
});

var endpointInstance = await Endpoint.Start(endpointConfiguration);
builder.Services.AddSingleton<IMessageSession>(endpointInstance);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

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

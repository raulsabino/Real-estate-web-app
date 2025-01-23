using RealEstateWebApp.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapListingsEndpoints();

app.Run();

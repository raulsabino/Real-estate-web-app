using RealEstateWebApp.Api.Data;
using RealEstateWebApp.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("RealEstate");
builder.Services.AddSqlite<RealEstateContext>(connString);

var app = builder.Build();

app.MapListingsEndpoints();

app.MigrateDb();

app.Run();

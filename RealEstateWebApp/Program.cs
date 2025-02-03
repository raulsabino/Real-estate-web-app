using RealEstateWebApp.Api.Data;
using RealEstateWebApp.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalDev",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("http://localhost:5500", "http://127.0.0.1:5500")
                         .AllowAnyHeader()
                         .AllowAnyMethod();
        });
});

var connString = builder.Configuration.GetConnectionString("RealEstate");
builder.Services.AddSqlite<RealEstateContext>(connString);

var app = builder.Build();

app.UseCors("AllowLocalDev");
app.UseStaticFiles();

// POST /upload-image
app.MapPost("/upload-image", async (HttpContext httpContext) => {
    var form = await httpContext.Request.ReadFormAsync();
    var file = form.Files["image"];

    if (file == null) return Results.BadRequest("No file uploaded");

    string uploadsDir = Path.Combine("wwwroot", "uploads");
    if (!Directory.Exists(uploadsDir)) {
        Directory.CreateDirectory(uploadsDir);
    }

    string filePath = Path.Combine(uploadsDir, Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

    using (var stream = new FileStream(filePath, FileMode.Create)) {
        await file.CopyToAsync(stream);
    }

    string fileUrl = $"http://localhost:5240/uploads/{Path.GetFileName(filePath)}";
    return Results.Ok(fileUrl);
});

app.MapListingsEndpoints();

await app.MigrateDbAsync();

app.Run();

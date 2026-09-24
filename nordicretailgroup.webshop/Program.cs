using Microsoft.AspNetCore.Authentication.JwtBearer;
using nordicretailgroup.webshop.Application;
using nordicretailgroup.webshop.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority =
            "https://ygpuhlqdoanzygbckgmv.supabase.co/auth/v1";

        options.Audience = "authenticated";
    });

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Nordic Retail Group Webshop API",
        Version = "v1"
    });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAuthorization();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();





app.MapControllers();

app.Run();
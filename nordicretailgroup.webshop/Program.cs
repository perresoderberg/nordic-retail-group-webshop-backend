using Microsoft.AspNetCore.Authentication.JwtBearer;
using nordicretailgroup.webshop.Application;
using nordicretailgroup.webshop.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority =
            "https://ygpuhlqdoanzygbckgmv.supabase.co/auth/v1";

        options.Audience = "authenticated";

        options.TokenValidationParameters.RoleClaimType = "user_role";
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "https://my-hosted-frontend.onrender.com" // just an example !!!!!
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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



var app = builder.Build();

app.UseCors("Frontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
using FirstApi;
using FirstApi.Context;
using FirstApi.Repositories.Implementation;
using FirstApi.Repositories.Interfaces;
using FirstApi.Services.Implementation;
using FirstApi.Services.Interfaces;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//tanzimat service serilog ke khodemoon ezafe kardim
//y file dar masir proje ijad kard ke log haro onja mindaze be soorat roozane ham avaz mishe
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("log/cityinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

//inja migim az in logger khodam estefade kon bja logger pishfarze khode core
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CityContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CityContext")));
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IPointRepository, PointRepository>();
builder.Services.AddSingleton<CityDataStore>();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
builder.Services.AddScoped<IMailService,LocalMailService>();
builder.Services.AddScoped<IAuthentication, Authentication>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//in tanzimat yani model va dto hame dar yek laye hastan yani kolan y laye darim

//tanzimat marboot be JWTBearer
builder.Services.AddAuthentication("Bearer").AddJwtBearer(option =>
{
    option.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

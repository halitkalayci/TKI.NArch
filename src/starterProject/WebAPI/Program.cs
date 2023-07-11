using Application;
using Core.Security;
using Persistence;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using Core.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Core.Security.Encryption;
using Microsoft.OpenApi.Models;
using Infrastructure;
using Hangfire;
using Application.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSecurityServices();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR(opt =>{});
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization"
    });

    opt.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        { new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        }, new string[] { } }
    });
});
builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireDb"));
});
builder.Services.AddHangfireServer();
builder.Services.AddCors(opt => opt.AddDefaultPolicy(p=> p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

const string tokenOptionsConfigurationSection = "TokenOptions";
TokenOptions tokenOptions = builder.Configuration.GetSection(tokenOptionsConfigurationSection).Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer=true,
            ValidateAudience=true,
            ValidateLifetime=true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience= tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey= SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseRouting();

app.UseHangfireDashboard("/hangfire");

app.UseAuthorization();
app.UseCors(opt => opt.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials());

app.UseEndpoints((endpoints) =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/api/chathub");
    // Hub endpointleri maple
});
/*
// Loglamalar
// Kiralama sonrasý kullanýcýya hatýrlatma mesajý => CreateRentalCommand.cs
var fireandForgetJobId = BackgroundJob.Enqueue(() => Console.WriteLine("Background Job Çalýþtý")); // Fire and Forget
var scheduleJobId = BackgroundJob.Schedule(() => Console.WriteLine("Background job schedule çalýþtý"), TimeSpan.FromSeconds(60)); // Delayed Jobs => Belirli bir vakit sonrasý çalýþtýrýlmasýný istediðimiz iþler
RecurringJob.AddOrUpdate("logger", () => Console.WriteLine("Loggger çalýþtý."), Cron.Minutely);
BackgroundJob.ContinueJobWith(fireandForgetJobId, () => Console.WriteLine("FireAndForget bitti.")); // Continuations
*/
app.Run();

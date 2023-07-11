using Infrastructure.FileUpload.Adapters;
using Infrastructure.FileUpload.Services.Cloudinary;
using Infrastructure.Payment.Adapters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;
public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // FindeksService, TCKN doğrulama
        services.AddScoped<IPosServiceAdapter, IyziCoPosServiceAdapter>();
        services.AddScoped<IFileUploadAdapter, CloudinaryAdapter>();
        return services;
    }
}

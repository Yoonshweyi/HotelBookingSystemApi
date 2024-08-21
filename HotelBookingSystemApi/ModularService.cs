using HotelBookingSystem.DBService.Model;
using HotelBookingSystem.Model;
using HotelBookingSystem.Shared;
using HotelBookingSystemApi.Feature.Authentication.Login;
using HotelBookingSystemApi.Feature.Booking;
using HotelBookingSystemApi.Feature.Payment;
using HotelBookingSystemApi.Feature.Room;
using HotelBookingSystemApi.Feature.RoomCategory;
using HotelBookingSystemApi.Feature.User;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystemApi
{
    public static class ModularService
    {
        public static IServiceCollection AddServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAppDbContextService(builder);
            services.AddJwtTokenGenerateServices();
            services.AddDataAccessServices();
            services.AddBusinessLogicServices();
            return services;
        }

        private static IServiceCollection AddAppDbContextService(this IServiceCollection services,
        WebApplicationBuilder builder)
        {
            services.AddDbContext<AppDbContext>(
                opt => { opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")); },
                ServiceLifetime.Transient,
                ServiceLifetime.Transient);

            return services;
        }
        private static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<BL_User>();
            services.AddScoped<BL_Login>();
            services.AddScoped<BL_RoomCategory>();
            services.AddScoped<BL_Room>();
            services.AddScoped<BL_Booking>();
            services.AddScoped<BL_Payment>();
            services.AddScoped<ResponseModel>();
            return services;
        }
        private static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<DL_User>();
            services.AddScoped<DL_Login>();
            services.AddScoped<DL_RoomCategory>();
            services.AddScoped<DL_Room>();
            services.AddScoped<DL_Booking>();
            services.AddScoped<DL_Payment>();
            return services;
        }
        private static IServiceCollection AddJwtTokenGenerateServices(this IServiceCollection services)
        {
            services.AddScoped<JwtTokenGenerate>();
            return services;
        }
    }
}

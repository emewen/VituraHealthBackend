
using VituraHealthBackend.Services;

namespace VituraHealthBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var LocalhostAllowSpecificOrigins = "_localhostOrigins";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton<IPatientService, PatientService>();
            builder.Services.AddSingleton<IPrescriptionService, PrescriptionService>();
            builder.Services.AddSingleton<ICacheService, CacheService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: LocalhostAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:5173").AllowAnyHeader();
                                  });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(LocalhostAllowSpecificOrigins);

            app.MapControllers();

            app.Run();
        }
    }
}

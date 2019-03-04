using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfficeBranchApi.Condext;
using OfficeBranchApi.Service;

namespace OfficeBranchApi
{
    public class Startup
    {
        

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           
        }

        public IConfiguration Configuration { get; }
       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200"));
            });
            services.AddDbContext<DbContextRepo>(opt =>opt.UseSqlServer(Configuration.GetConnectionString("mysqlConnectionString")));

            services.AddScoped<IEmployeeRestService, EmployeeRestService>();
            services.AddScoped<IEquipmentsRestService, EquipmentsRestService>();
            services.AddScoped<IEquipmentTypeService, EquipmentTypeService>();
            services.AddScoped<IOfficeBranchService, OfficeBranchService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IPositionToEquipmentService, PositionToEquipmentService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

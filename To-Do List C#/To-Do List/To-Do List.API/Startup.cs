using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Net;
using To_Do_List.Application.Interfaces;
using To_Do_List.Application.Profiles;
using To_Do_List.Application.Services;
using To_Do_List.Infrastructure.Data;
using To_Do_List.Infrastructure.Interface;
using To_Do_List.Infrastructure.Repositories;
using To_Do_List.Infrastructure.UOW;

namespace To_Do_List.API
{
    public class Startup
    {
        readonly string MyAllowSpeficifOrigins = "_myAllowSpeficifOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllersWithViews();

            // Add DbContext
            services.AddDbContext<DataContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("Sql")));

            /// Add UOW
            services.AddScoped<UnitOfWork>();

            /// Add repositories
            services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<TareaRepository>();

            /// Add Services
            services.AddScoped<ITareaService, TareaService>();

            // Add Automapper
            services.AddAutoMapper(typeof(TareaProfile));

            // Add AddCors
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpeficifOrigins,
                    builder =>
                    {
                        builder.WithOrigins("*").WithMethods("*").WithHeaders("*");
                    });
            });

            // Add Swagger
            /*
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "To-Do List",
                    Version = "v1",
                    Description = $"To-Do List, creado con tecnologia NETCore 5.0 \n " +
                    $"\n -Se siguio un patron por capas, con un patron de Generic Repository y Unit of Work \n" +
                    $"\n -Utilizo un modelado de base de datos utilizando EF Core y un patron CodeFirst \n " +
                    $"\n -Se agrega tambien inyeccion de dependendias para darle mas claridad de responsabilidad a cada clase \n" +
                    $"\n -Se utiliza la libreria de Automapper para trabajar los DTOs de manera automatizada \n",
                });
            });
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                /*
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "To_Do_List v1"));
                */
            }

            /// Handlererrors
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; ;
                        context.Response.ContentType = "text/html";

                        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                        await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                        var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();

                        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                        {
                            await context.Response.WriteAsync(
                                                      "File error thrown!<br><br>\r\n");
                        }
                        await context.Response.WriteAsync(
                                                      "<a href=\"/\">Home</a><br>\r\n");
                        await context.Response.WriteAsync("</body></html>\r\n");
                        await context.Response.WriteAsync(new string(' ', 512));
                    });
                });
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(MyAllowSpeficifOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                /*
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("To-Do List");
                });
                */
            });
        }
    }
}

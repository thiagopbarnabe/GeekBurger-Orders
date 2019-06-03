using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeekBurger.Orders.Extensions;
using GeekBurger.Orders.Repository;
using GeekBurger.Orders.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace GeekBurger.Orders
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var mvcCoreBuilder = services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Orders",
                    Version = "v1"
                });
            });

            services.AddDbContext<OrdersContext>(x => x.UseInMemoryDatabase("geekburger-orders"));
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            
            //services.AddSingleton<IOrderChangedService, OrderChangedService>();
            //services.AddSingleton<ILogService, LogService>();

            services.AddAutoMapper();

            //mvcCoreBuilder
            //    .AddFormatterMappings()
            //    .AddJsonFormatters()
            //    .AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, OrdersContext ordersContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ordersContext.Seed();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
               "Orders");
            });
        }
    }
}

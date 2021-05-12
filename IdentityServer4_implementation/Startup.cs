using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4_implementation.Models;
using IdentityServer4_implementation.Resources;
using IdentityServer4_implementation.Services.TokenProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace IdentityServer4_implementation
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
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "IdentityServer4_implementation", Version = "v1"});
            });
            services.AddIdentityServer(options => options.IssuerUri = "localhost")
                .AddInMemoryApiResources(ClientStore.GetApiResources())
                .AddInMemoryApiScopes(ClientStore.GetApiScopes())
                .AddInMemoryIdentityResources(ClientStore.GetIdentityResources())
                .AddInMemoryClients(ClientStore.GetClients())
                .AddTestUsers(Users.GetTestUsers())
                .AddDeveloperSigningCredential(false);
            services.AddTransient<ITokenProvider, TokenProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityServer4_implementation v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            
            app.UseIdentityServer();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
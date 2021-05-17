using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using IdentityServer4_implementation.Models;
using IdentityServer4_implementation.Resources;
using IdentityServer4_implementation.Services;
using IdentityServer4_implementation.Services.TokenProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            // Для пользователей
            services.AddDbContext<SqlDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MainConnection")));
            
            
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
                .AddProfileService<ProfileService>()
                //.AddTestUsers(Users.GetTestUsers())
                .AddDeveloperSigningCredential(false);
            services.AddTransient<ITokenProvider, TokenProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com";
                    options.ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo";
                    options.CallbackPath = "/signin-google";
                });
            
            // Для пользователей
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();
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
            
            // Для пользователей
            /*JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            IdentityServerAuthenticationOptions identityServerAuthenticationOptions =
                new IdentityServerAuthenticationOptions
                {
                    Authority = "http://localhost:5000",
                    ApiSecret = "login_secret",
                    ApiName = "all",
                    SupportedTokens = SupportedTokens.Both,
                    
                    RequireHttpsMetadata = false
                };
            app.UseIdentityServerAuthentication(identityServerAuthenticationOptions);*/

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
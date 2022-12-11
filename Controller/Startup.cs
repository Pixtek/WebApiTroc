using System.Text;
using Application.UseCases.Users;
using Application.UseCases.Users.Dto;
using Infrastructure.EF;
using Infrastructure.EF.Article;
using Infrastructure.EF.Category;
using Infrastructure.EF.Commentary;
using Infrastructure.EF.JwtAuthentication;
using Infrastructure.EF.Transaction;
using Infrastructure.EF.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace WebApiTroc;

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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JwtApi", Version = "v1" });
            });
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
                
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["cookie"];
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddTransient<IJwtAuthenticationService, JwtAuthenticationService>();
            services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();
            services.AddScoped<IArticle, ArticleRepository>();
            services.AddScoped<IUsers, UsersRepository>();
            services.AddScoped<TrocContextProvider>();

            //users
            services.AddScoped<UseCaseFetchById>();
            services.AddScoped<UseCaseCreateUser>();
            services.AddScoped<UseCaseFetchByPseudo>();

            //Commentary
            services.AddScoped<ICommentary, CommentaryRepository>();

            //Transactions
            services.AddScoped<ITransaction, TransactionRepository>();
            
            //category
            services.AddScoped<ICategory, CategoryRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("Dev", policyBuilder =>
                {
                    policyBuilder.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JwtApi v1"));
            }

            app.UseHttpsRedirection();
            
            app.UseCors("Dev");

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
}
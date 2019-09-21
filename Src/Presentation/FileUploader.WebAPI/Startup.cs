using System.Reflection;
using AutoMapper;
using FileUploader.Application.Commands;
using FileUploader.Application.Contracts.Commands;
using FileUploader.Application.Contracts.Context;
using FileUploader.Application.Contracts.Infrastructure;
using FileUploader.Application.Contracts.Infrastructure.AutoMapper;
using FileUploader.Application.Contracts.Queries;
using FileUploader.Application.Queries;
using FileUploader.Infrastructure.Implementations;
using FileUploader.Persistence.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FileUploader.WebAPI
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
            services
                .AddCors()
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Add DbContext using SQL Server Provider
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnectionString"));
                options.EnableSensitiveDataLogging();
            }, ServiceLifetime.Transient);

            // Add AutoMapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            // Add framework services
            services.AddTransient<IFileImport, FileImport>();

            // Add Commands & Queries (CQRS)
            services.AddScoped<IArticlePriceQueryService, ArticlePriceQueryService>();

            services.AddScoped<IArticleCommandService, ArticleCommandService>();
            services.AddScoped<IArticlePriceCommandService, ArticlePriceCommandService>();
            services.AddScoped<IColorCommandService, ColorCommandService>();
            services.AddScoped<IDeliveredInCommandService, DeliveredInCommandService>();
            services.AddScoped<ISectionCommandService, SectionCommandService>();
            services.AddScoped<ISizeCommandService, SizeCommandService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

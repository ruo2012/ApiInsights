﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cheers.ApiInsights;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Config;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiInsights();
            services.AddMvc();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();

            //全局的 HttpContext
            app.UseGlobalHttpContext();

            //省略了其他配置

            LogManager.Configuration = new XmlLoggingConfiguration(Path.Combine(env.ContentRootPath, "nlog.config"));
            LogManager.Configuration.Variables["root"] = env.ContentRootPath;
            LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("DefaultConnection");

        }
    }
}

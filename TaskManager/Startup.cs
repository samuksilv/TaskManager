using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
// using Swashbuckle.AspNetCore.Swagger;
using TaskManager.Context;

namespace TaskManager
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
           services.Configure<GzipCompressionProviderOptions>(options => {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(Options=>{
                Options.Providers.Add<GzipCompressionProvider>();
                Options.EnableForHttps= true;
            });

             services.AddSingleton<IConfiguration>(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);            
            
            services.AddEntityFrameworkNpgsql().AddDbContextPool<TaskManagerContext>(opt=> 
                opt.UseNpgsql(Configuration.GetConnectionString("conPostgree" ), 
                o=> o.MaxBatchSize(int.MaxValue))
            );

            //Configure Swagger
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new Info {
                    Title = "TaskManager API",
                        Version = "v1",
                        Description = " "                    
                });           

                var xmlFile = Path.ChangeExtension (typeof (Startup).Assembly.Location, ".xml");
                c.IncludeXmlComments (xmlFile);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {               
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseMvc ();
            
            var loggingOptions = this.Configuration.GetSection("Log4NetCore")
                                               .Get<Log4NetProviderOptions>();
            loggerFactory.AddLog4Net(loggingOptions);


            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "Swagger Sample");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

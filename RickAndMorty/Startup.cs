using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Service;

namespace RickAndMorty
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //Named HttpClient
            services.AddHttpClient("RickAndMortyClient", client =>
            {
                //configure client here
                client.BaseAddress = new Uri("https://rickandmortyapi.com/api/");
            });

            //Type HttpClient 
            services.AddHttpClient<IRickAndMortyService, RickAndMortyService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

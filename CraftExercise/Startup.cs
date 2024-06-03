using CraftExercise.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CraftExercise
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
            services.AddRazorPages();

            // GitHub API
            services.AddHttpClient<GitHubService>(client =>
            {
                client.DefaultRequestHeaders.Add("User-Agent", "GitHubFreshdeskIntegrationApp");
                client.DefaultRequestHeaders.Add("Authorization", $"token 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");
            });

            // Freshdesk API
            services.AddHttpClient<FreshdeskService>(client =>
            {
                client.BaseAddress = new Uri("https://mgusvetiivanrilski.freshdesk.com/a/getstarted");
                client.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"OnR1FRF1EdJfp3j1k72k:X"))}");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
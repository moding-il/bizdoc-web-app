using BizDoc.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddBizDoc(options =>
{
    options.LicenseKey = "your-key-here";
    options.ApplicationUri = "http://";
    options.Smtp = new SmtpSettings
    {
        SenderAddress = new System.Net.Mail.MailAddress("bizdoc@moding.com"),
    };
}).
               UseSqlServer(connectionString).
            AddFormIdentity(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
            })
        .AddDbContext<CustomStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseDeveloperExceptionPage();
app.UseBizDoc().
UseFormIdentity();
app.UseSpa(spa =>
{
    // To learn more about options for serving an Angular SPA from ASP.NET Core,
    // see https://go.microsoft.com/fwlink/?linkid=864501

    spa.Options.SourcePath = "ClientApp";
    spa.Options.StartupTimeout = TimeSpan.FromMinutes(2);

    if (app.Environment.IsDevelopment())
    {
        spa.UseAngularCliServer(npmScript: "start");
    }
});
app.UseCors();

PrepareCustomStore();

app.Run();

void PrepareCustomStore()
{
    using var serviceScope = app.Services.GetService<IServiceScopeFactory>()
                    .CreateScope();
    var store = serviceScope.ServiceProvider.GetService<CustomStore>();
    store.Database.Migrate();
    if (app.Environment.IsDevelopment())
    {
        new SeedData(store).Execute();
    }
}

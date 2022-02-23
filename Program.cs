using BizDoc.Core.Configuration;
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
app.UseCors();
app.MapFallbackToFile("index.html"); ;

app.Run();

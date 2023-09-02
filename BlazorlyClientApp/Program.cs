using Blazorly.ClientApplication.Core;
using BlazorlyClientApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
AppConfigs.DatabaseType = builder.Configuration.GetValue<string>("DatabaseType");
AppConfigs.DatabaseConnectionString = builder.Configuration.GetValue<string>("DatabaseConnectionString");
AppConfigs.DBTimeout = builder.Configuration.GetValue<int>("DBTimeout");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "api",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;
AppConfigs.Schema = new DBFactory(AppConfigs.DatabaseType, AppConfigs.DatabaseConnectionString, AppConfigs.DBTimeout).GetSchema();
app.Run();

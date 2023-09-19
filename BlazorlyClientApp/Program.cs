using Blazorly.ClientApplication.Core;
using BlazorlyClientApp;
using BlazorlyClientApp.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<TooltipService>();

AppConfig.DBConnectionString =  builder.Configuration.GetValue<string>("DBConnectionString");
AppConfig.DBType = builder.Configuration.GetValue<string>("DBType");
AppConfig.DBTimeout = builder.Configuration.GetValue<int>("DBTimeout");
AppConfig.Modules = builder.Configuration.GetSection("Modules").Get<List<string>>();
AppConfig.Schema = AppConfig.Factory.GetSchema();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
AppConfig.LoadModules();
app.Run();

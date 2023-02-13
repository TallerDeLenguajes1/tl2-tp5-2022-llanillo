global using static tp5.Util.SessionUtil;
global using tp5.Repositories.Interface;
global using tp5.Repositories.Impl;
global using tp5.Models;
global using NLog;
global using AutoMapper;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Data.Sqlite;
global using System.Diagnostics;
global using System.ComponentModel.DataAnnotations;
global using tp5.ViewModels.Usuario.Cadete;
global using tp5.ViewModels.Usuario.General;
global using tp5.ViewModels.Pedido;
global using tp5.ViewModels.Home;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddLogging();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<RepositorioUsuarioBase, RepositorioUsuario>();
builder.Services.AddTransient<RepositorioPedidoBase, RepositorioPedido>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(3);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();
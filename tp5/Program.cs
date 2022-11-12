global using static tp5.Util.CsvHelper;
global using tp5.ViewModels.Cadete;
global using tp5.ViewModels.Pedido;
global using tp5.Repositories;
global using tp5.Models;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Data.Sqlite;
global using AutoMapper;
global using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMvc();
// builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddTransient<Repositorio<Cadete>, CadeteRepositorio>();
builder.Services.AddTransient<Repositorio<Pedido>, PedidoRepositorio>();
builder.Services.AddTransient<Repositorio<Cliente>, ClienteRepositorio>();

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

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();
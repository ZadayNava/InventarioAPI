using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Inventario.Data;
using DAO_Controlador;
using Modelo;
using DAO_Controlador.ManagerController;
using Modelo.Modelos;


var builder = WebApplication.CreateBuilder(args);

var inventarioConection = builder.Configuration.GetConnectionString("InventarioConection");

builder.Services.AddControllers();
builder.Services.AddDbContext<DBContexto>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("InventarioConection")));


//agregar alcance de los cotroladores
builder.Services.AddScoped<IRepositorioGenerico<Producto>, DAOProducto>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();


app.MapControllers();

app.Run();

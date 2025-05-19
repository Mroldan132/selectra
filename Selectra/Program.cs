using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Selectra.Models;
using Selectra.Services;
using Selectra.Services.Areas;
using Selectra.Services.Cargos;
using Selectra.Services.Notificaciones;
using Selectra.Services.Personales;
using Selectra.Services.Requerimiento;
using Selectra.Services.Usuarios;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


//Implementaciones de interfaces

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRequerimientoPersonalService, RequerimientoPersonalService>();
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<ICargosService,CargosServices>();
builder.Services.AddScoped<IPersonalesServices, PersonalesServices>();
builder.Services.AddScoped<INotificacionesServices, NotificacionesServices>();

//Conexion a la base de datos
builder.Services.AddDbContext<SelectraContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SelectraConnection")));

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var llavesecreta = jwtSettings["SecretKey"];

//Autentificacion con JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(llavesecreta))
    };
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Cors

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:8080",
                                              "http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

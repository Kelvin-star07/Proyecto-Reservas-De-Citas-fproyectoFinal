
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infraestructura.Contexto;
using Microsoft.EntityFrameworkCore;
using Aplicacion.Interfaces;
using Infraestructura.Repositorios;
using Aplicacion.Servicios;

namespace Presentacion_reservas_citasss
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
               
                
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //configurar Json web token para que funcione en swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Ingrese 'Bearer' [espacio] y luego su token."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
         {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
        });

            }
            );



            // Configuracion de la conexionDB y dbcontext
            var conn = builder.Configuration.GetConnectionString("Appconnection");
            builder.Services.AddDbContext<ReservaCitasDbContext>(x => x.UseSqlServer(conn));



            //Cofiguraciones mis servicios de inyecciones de dependecias
            builder.Services.AddScoped<IRegistroUsuarioRepositorio, RegistroUsuarioRepositorio>();
            builder.Services.AddScoped<IRegistroUsuarioServicio, RegistroUsuarioServicio>();
            builder.Services.AddScoped<IConfiguracionReservaServicio, ConfiguracionReservaServicio>();
            builder.Services.AddScoped<IConfiguracionReservaRepositorio,ConfiguracionReservaRepositorio>();
            builder.Services.AddScoped<IGeneracionSlotServicio, GeneracionSlotServicio>();
            builder.Services.AddScoped<IGeneracionSlotsRepositorio, GeneracionSlotRepositorio>();
            builder.Services.AddScoped<IEstacionServicio, EstacionServicio>();
            builder.Services.AddScoped<IEstacionRepositorio, EstacionRepositorio>();
            builder.Services.AddScoped<ILeerLogServicio, LeerLogServicio>();
            builder.Services.AddScoped<IReservaCitasServicio, ReservaCitasServicio>();
            builder.Services.AddScoped<IReservaCitaRepositorio, ReservaCitaRepositorio>();
            builder.Services.AddScoped<ReservaCitasServicio>();
            builder.Services.AddScoped<LeerLogServicio>();
            builder.Services.AddScoped<ConfiguracionReservaRepositorio>();
            builder.Services.AddScoped<GeneracionSlotRepositorio>();
            builder.Services.AddScoped<EnvioGmailServicio>();
            builder.Services.AddScoped<GeneracionSlotServicio>();
            builder.Services.AddScoped<GeneracionTokenServicio>();
            builder.Services.AddScoped<RegistroUsuarioServicio>();
            builder.Services.AddScoped<ConfiguracionReservaServicio>();
            builder.Services.AddScoped<EstacionServicio>();
            builder.Services.AddScoped<EstacionRepositorio>();






            builder.Services.AddSwaggerGen();


            // Configuracion del jwt
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),


                };


                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                        logger.LogError(context.Exception, "Token inválido");
                        return Task.CompletedTask;
                    }
                };

            });



            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5500") 
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowFrontend");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

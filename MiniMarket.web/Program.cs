using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MiniMarket.web.Services;
using MiniMarket.web.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// ============================================
// 🔥 CONFIGURACIÓN DE CORS
// ============================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
        });
});

// ============================================
// AUTOMAPPER - MAPPINGS
// ============================================
builder.Services.AddAutoMapper(typeof(Program));

// ============================================
// SERVICIOS - INYECCIÓN DE DEPENDENCIAS
// ============================================
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IVentaService, VentaService>();
builder.Services.AddScoped<IClienteService, ClienteService>();

// ============================================
// FLUENTVALIDATION - VALIDADORES
// ============================================
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// ============================================
// JWT - AUTENTICACIÓN
// ============================================
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ??
    throw new InvalidOperationException("JWT Key no configurada en appsettings.json"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ============================================
// SWAGGER
// ============================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ============================================
// 🔥 CORS DEBE IR ANTES DE AUTENTICACIÓN
// ============================================
app.UseCors("AllowAll");

// ============================================
// AUTENTICACIÓN Y AUTORIZACIÓN
// ============================================
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NArcBackEnd.Business.DependencyResolvers.Autofac;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//autofac --- autofac ve extension.dependencyinjection kurulur.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", //bir isim verme!
                                     //builder =>builder.WithOrigins("https://localhost:4200") //eri�im olacak yerler verilir. //site bazl� izin.
        builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()// her�eye izin ver demektir.
        ); //izin verilme �artlar�
});

//jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true, //tokeni kimin kontrol edip etmeyece�ini bearer kontrol edecek mi?
        ValidateIssuer = true, //olu�turulacak token de�eri kimden ald� kontrol.
        ValidateLifetime = false, //token i�inde bir experience s�resi olacak m� 
        ValidateIssuerSigningKey = true, // �retilecek token de�erinin uygulamam�za ait oldu�unu g�sterir.
        ValidIssuer = builder.Configuration["Token:Issuer"], //olu�turulacak token de�erinin kimin da��tt���n� g�sterir.
        ValidAudience = builder.Configuration["Token:Audience"], //olu�turalacak token de�erinin hangi sitenin ula�abilece�i
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])), //token de�erinin bize ait oldu�unu do�rulayan yap�d�r. yani olu�turankey bize ait odur.
        ClockSkew = TimeSpan.Zero //expression s�resine bu s�reyi ekler. server ile bilgisayar aras� farkdan dolay� bu var.
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin"); //corsa verilen isim verilir. uygulaman�n as�l aya�a kalkt��� yer buras�d�r. uygulamaya izin veriyorsun.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

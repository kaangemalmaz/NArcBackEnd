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
                                     //builder =>builder.WithOrigins("https://localhost:4200") //eriþim olacak yerler verilir. //site bazlý izin.
        builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()// herþeye izin ver demektir.
        ); //izin verilme þartlarý
});

//jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true, //tokeni kimin kontrol edip etmeyeceðini bearer kontrol edecek mi?
        ValidateIssuer = true, //oluþturulacak token deðeri kimden aldý kontrol.
        ValidateLifetime = false, //token içinde bir experience süresi olacak mý 
        ValidateIssuerSigningKey = true, // üretilecek token deðerinin uygulamamýza ait olduðunu gösterir.
        ValidIssuer = builder.Configuration["Token:Issuer"], //oluþturulacak token deðerinin kimin daðýttýðýný gösterir.
        ValidAudience = builder.Configuration["Token:Audience"], //oluþturalacak token deðerinin hangi sitenin ulaþabileceði
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])), //token deðerinin bize ait olduðunu doðrulayan yapýdýr. yani oluþturankey bize ait odur.
        ClockSkew = TimeSpan.Zero //expression süresine bu süreyi ekler. server ile bilgisayar arasý farkdan dolayý bu var.
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin"); //corsa verilen isim verilir. uygulamanýn asýl ayaða kalktýðý yer burasýdýr. uygulamaya izin veriyorsun.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

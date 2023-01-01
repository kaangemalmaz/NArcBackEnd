using Autofac;
using Autofac.Extensions.DependencyInjection;
using NArcBackEnd.Business.DependencyResolvers.Autofac;

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

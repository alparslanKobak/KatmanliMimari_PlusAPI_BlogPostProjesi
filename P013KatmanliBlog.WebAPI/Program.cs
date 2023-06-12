using P013KatmanliBlog.Data;
using P013KatmanliBlog.Service.Abstract;
using P013KatmanliBlog.Service.Concrete;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(x=> x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); // Ýç içe girmiþ döngüleri görmezden gelmenin bir formülüdür. Örneðin Post classý altýndaki User ve Category class'ýna sonsuza kadar girmeye çalýþmaktansa bir defa girer býrakýr.



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));

builder.Services.AddTransient<IPostService, PostService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.AspNetCore.Http.Timeouts;
using MongoDB.Driver;
using TodoListService.Interfaces;
using TodoListService.Managers;
using TodoListService.Models;
using TodoListService.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


MongoDbConfigure(builder.Services);
builder.Services.AddTransient<IUserManager, UserManager>();
builder.Services.AddTransient<ITodoListManager, TodoListManager>();


builder.Services.AddRequestTimeouts(options => {
    options.DefaultPolicy = new RequestTimeoutPolicy { Timeout = TimeSpan.FromSeconds(20) };
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
app.MapControllers(); 

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("../swagger/v1/swagger.json", "Test API V1");
        c.RoutePrefix = string.Empty;
    });


app.Run();


void MongoDbConfigure(IServiceCollection services)
{
    try
    {
        var mongoDbSettings = new MongoDbSettingsModel
        {
            AtlasUri = Environment.GetEnvironmentVariable("MongoDbAtlasUri"),
            DatabaseName = Environment.GetEnvironmentVariable("MongoDbName")
        };

        var client = new MongoClient(mongoDbSettings.AtlasUri);
        var database = client.GetDatabase(mongoDbSettings.DatabaseName);
    
        services.AddSingleton(database);
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ITodoListRepository, TodoListRepository>();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}

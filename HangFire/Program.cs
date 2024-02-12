using Hangfire;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
 
builder.Services.AddHangfire(configuration: cnf =>
{
    cnf.UseSqlServerStorage(nameOrConnectionString: builder.Configuration["ConnectionStrings:HangFireConnection"]);
    cnf.UseSimpleAssemblyNameTypeSerializer();
    cnf.UseRecommendedSerializerSettings();
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
builder.Services.AddControllers();

builder.Services.AddHangfireServer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}
app.UseHttpsRedirection();

app.MapControllers();
//app.MapGet("/", () => "Hello World!");
app.UseHangfireDashboard(pathMatch: "/dashboard");

app.Run();

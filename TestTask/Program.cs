using TestTask.Data;
using TestTask.Models;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

builder.Services
    .AddDbContext<GameDbContext>()
    .AddScoped<IRepository<GameModel>, GameRepository>()
    .AddAutoMapper(typeof(Program))
    .AddControllers();

#endregion

var app = builder.Build();

#region Mapping
var mapper = app.Services.GetRequiredService<AutoMapper.IConfigurationProvider>();
if (app.Environment.IsDevelopment())
{
    // validate Mapper Configuration
    mapper.AssertConfigurationIsValid();
}
else
{
    mapper.CompileMappings();
}
#endregion

#region DataSeeding
using (var scope = app.Services.CreateScope())
{
    var dataSeeder = new DataSeeder(scope.ServiceProvider.GetRequiredService<IRepository<GameModel>>());
    await dataSeeder.Seed();
}
#endregion

#region Configure the HTTP request pipeline

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

#endregion

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
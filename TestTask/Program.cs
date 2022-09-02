using TestTask.Data;
using TestTask.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .Services.AddDbContext<GameDbContext>()
    .AddScoped<IRepository<GameModel>, GameRepository>()
    .AddAutoMapper(typeof(Program));

var app = builder.Build();

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

using (var scope = app.Services.CreateScope())
{
    var dataSeeder = new DataSeeder(scope.ServiceProvider.GetRequiredService<IRepository<GameModel>>());
    await dataSeeder.Seed();
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
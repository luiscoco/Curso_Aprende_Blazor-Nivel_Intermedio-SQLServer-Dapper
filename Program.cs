using BlazorWebAppWithDapper.Components;
using BlazorWebAppWithDapper.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure the connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register the database connection factory
builder.Services.AddScoped<IDbConnectionFactory>(_ => new SqlDbConnectionFactory(connectionString));

// Register the ProductsRepository
builder.Services.AddScoped<ProductsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

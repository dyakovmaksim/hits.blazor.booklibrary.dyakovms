using BookLibraryApp.Components;
using BookLibraryApp.Data;
using BookLibraryApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ================================
// 🔧 SERVICES
// ================================

// Razor Components (Blazor)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// EF Core + SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Data/app.db"));

// Business services (DI)
builder.Services.AddScoped<IBookService, BookService>();

// Generic repository (DI + generics)
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));


// ================================
// 🚀 BUILD APP
// ================================

var app = builder.Build();


// ================================
// 🗄️ INIT DATABASE
// ================================

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();        // создаёт Data/app.db
    SeedData.AddInitialData(db);        // наполняет тестовыми данными
}


// ================================
// 🌐 MIDDLEWARE
// ================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

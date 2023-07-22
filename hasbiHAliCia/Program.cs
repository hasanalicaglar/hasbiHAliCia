using hasbiHAliCia.Veri;
using hasbiHAliCia.SignalR;
using hasbiHAliCia.AraYazilimlar;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(ayarlar =>
{
    ayarlar.Cookie.Name = "hasbiHAliCia";
    ayarlar.IdleTimeout = TimeSpan.FromSeconds(10);
    ayarlar.Cookie.IsEssential = true;
});
builder.Services.AddDbContext<VeritabaniBaglami>(ServiceLifetime.Transient);
builder.Services.AddSignalR();
builder.Services.AddSingleton<SohbetHub>();
builder.Services.AddSingleton<IletilerAbonesi>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Anasayfa/Hata");
}


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseForwardedHeaders();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Anasayfa}/{action=Giris}/{id?}");

app.MapHub<SohbetHub>("/sohbetHub");
app.IletilerTabloBagimiKullan();

app.Run();

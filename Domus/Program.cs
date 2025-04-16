var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddDistributedMemoryCache(); // Almacenamiento en memoria
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(120); // Tiempo de expiraci�n
	options.Cookie.HttpOnly = true; // Seguridad
	options.Cookie.IsEssential = true; // Necesario para GDPR
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

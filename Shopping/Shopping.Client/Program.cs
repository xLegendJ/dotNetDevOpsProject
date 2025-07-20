var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// --- INJECT THIS PART HERE ---
builder.Services.AddHttpClient("ShoppingAPIClient", client =>
{
    //client.BaseAddress = new Uri("http://localhost:5000/"); // Shopping.API url
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:ShoppingAPIUrl"]);
});
// --- END INJECTION ---

builder.Services.AddControllersWithViews(); // This line was already there and should remain

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
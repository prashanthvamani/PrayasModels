var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();



app.MapControllerRoute(
    name: "default",


    //pattern: "{controller=ReportSnowFlake}/{action=Index}/{id?}");
    //pattern: "{controller=SnowFlakeData}/{action=Index}/{id?}");
    pattern: "{controller=Login}/{action=Login}/{id?}");
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    //pattern: "{controller=BIreportsdownload}/{action=Index}/{id?}");

app.Run();

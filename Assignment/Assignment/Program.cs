using Assignment.IServices;
using Assignment.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProduct_Service, Product_Service>(); 
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(10);
    // cài session tồn tại trong 10p
});
/*
 * AddSingleton: N?u service ???c kh?i t?o, nó s? t?n t?i cho ??n khi vòng ??i
 * c?a ?ng d?ng k?t thúc. N?u các Repuest khác mà tri?n khai thì s? d?ng alij
 * chính service ?ó. PHù h?p cho các sercive có tính toàn c?c và không thay ??i
 * AddScope: M?i l?n có http Reuest thì s? t?o ra service 1 l?n và ???c gi?
 * nguyên trong quá trình repuest ???c x? lí. Lo?i này s? ???c s? d?ng cho các service
 * v?i nh?ng yêu c?u http c? th?
 * Add transient: T?o m?i service m?i khi có repuest. v?i m?i yêu c?u http s? 
 * nh?n ???c m?t ??i t??ng service khác nhau. Phù h?p cho các service mà có th?
 * ph?c v? nhi?u yêu c?u http repuest
 * 
 */
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
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

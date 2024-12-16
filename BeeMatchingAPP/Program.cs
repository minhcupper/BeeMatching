
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Cấu hình HttpClient
builder.Services.AddHttpClient();

// Cấu hình Antiforgery (bảo vệ CSRF)
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Đảm bảo cookie luôn được bảo mật
});

// Cấu hình Session
builder.Services.AddDistributedMemoryCache(); // Dùng bộ nhớ trong để lưu trữ session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thiết lập thời gian hết hạn session
    options.Cookie.HttpOnly = true; // Chỉ cho phép truy cập cookie qua HTTP
    options.Cookie.IsEssential = true; // Cần thiết cho yêu cầu GDPR
});
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();  // Ghi log vào Console
    logging.AddDebug();    // Ghi log vào Debug
});
builder.Host.ConfigureHostConfiguration(config =>
{
    config.AddInMemoryCollection(new[]
    {
        new KeyValuePair<string, string>("ASPNETCORE_ENVIRONMENT", "Development")
    });
});


var app = builder.Build();
Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");
// Cấu hình HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Kích hoạt HSTS (HTTP Strict Transport Security)
}

app.UseHttpsRedirection(); // Chuyển hướng HTTP về HTTPS
app.UseStaticFiles(); // Phục vụ các tệp tĩnh

app.UseRouting(); // Kích hoạt định tuyến
app.UseSession(); // Kích hoạt middleware session
app.UseAuthorization(); // Kích hoạt xác thực

// Cấu hình route mặc định cho controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseDeveloperExceptionPage();
app.Run(); // Chạy ứng dụng
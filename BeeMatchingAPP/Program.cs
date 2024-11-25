var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
         builder.Services.AddControllersWithViews();
        
        builder.Services.AddHttpClient();

        // Configure Antiforgery
        builder.Services.AddAntiforgery(options =>
        {
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure secure cookies
        });

        // Configure Session
        builder.Services.AddDistributedMemoryCache(); // Use in-memory cache for session storage
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
            options.Cookie.HttpOnly = true; // Enhance security
            options.Cookie.IsEssential = true; // Required for GDPR compliance
        });

        var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); // Redirect HTTP to HTTPS
        app.UseStaticFiles(); // Serve static files

        app.UseRouting(); // Enable routing
        app.UseSession(); // Enable session middleware
        app.UseAuthorization(); // Enable authorization

        // Map controller routes
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        app.Run();
    

using API_He_thong.DATA;
using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API_He_thong.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using BeeMatchingAPP.DATA;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add JWT authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,  // Set to true to validate lifetime of the token
        ValidateIssuerSigningKey = true,  // Validate signing key
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});


HttpClientHandler handler = new HttpClientHandler();
handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

HttpClient client = new HttpClient(handler);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
// Register Identity services
builder.Services.AddSingleton<IPasswordHasher<NguoiDung>, PasswordHasher<NguoiDung>>();

// Register your other services
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<API_Context>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("Dbconection")));
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IPlace, PlaceService>();
builder.Services.AddScoped<ICompany, CompanyService>();
builder.Services.AddScoped<INguoiXinViec, NguoiXinViecService>();
builder.Services.AddScoped<IDanhGia, DanhGiaService>();
builder.Services.AddScoped<IDanhMucKyNang, DanhMucKyNangService>();
builder.Services.AddScoped<IThongBao, ThongBaoService>();
builder.Services.AddScoped<IUngtuyen, UngTuyenService>();
builder.Services.AddScoped<ISkillNguoiXinViec, SkillNguoiXinViecService>();
builder.Services.AddScoped<ISkillCongViec, KyNangCongViecSerVice>();
builder.Services.AddScoped<ICongViec, CongViecService>();
builder.Services.AddScoped<IKinhNghiemNguoiXinViec, KinhNghiemNguoiXinViecService>();
builder.Services.AddScoped<Ikinhnghiemcongviec, KinhNghiemCongViecService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IWardService, WardService>();
builder.Services.AddScoped<IProvinceService, ProvinceService>();
builder.Services.AddScoped<IDistrictService, DistrictService>();
builder.Services.AddScoped<Ibangcapcongviec, BangCapCongViecSerVice>();
builder.Host.ConfigureHostConfiguration(config =>
{
    config.AddInMemoryCollection(new[]
    {
        new KeyValuePair<string, string>("ASPNETCORE_ENVIRONMENT", "Development")
    });
});


var app = builder.Build();
Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();  // Middleware to authenticate the request
app.UseAuthorization();   // Middleware to authorize the request

app.MapControllers();

app.Run();

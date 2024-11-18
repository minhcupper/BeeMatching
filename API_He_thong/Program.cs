using API_He_thong.DATA;
using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API_He_thong.Repositories;
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
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<API_Context>(op=>op.UseSqlServer(builder.Configuration.GetConnectionString("Dbconection")));
IServiceCollection serviceCollection1 = builder.Services.AddScoped<IUser, UserService>();
IServiceCollection serviceCollection2 = builder.Services.AddScoped<IPlace, PlaceService>();
IServiceCollection serviceCollection = builder.Services.AddScoped<ICompany, CompanyService>();
IServiceCollection serviceCollection4 = builder.Services.AddScoped<INguoiXinViec, NguoiXinViecService>();
IServiceCollection serviceCollection5 = builder.Services.AddScoped<IDanhGia, DanhGiaService>();
IServiceCollection serviceCollection6 = builder.Services.AddScoped<IDanhMucKyNang, DanhMucKyNangService>();
IServiceCollection serviceCollection7 = builder.Services.AddScoped<IThongBao, ThongBaoService>();
IServiceCollection serviceCollection8 = builder.Services.AddScoped<IUngtuyen, UngTuyenService>();
IServiceCollection serviceCollection9 = builder.Services.AddScoped<ISkillNguoiXinViec, SkillNguoiXinViecService>();
IServiceCollection serviceCollection10 = builder.Services.AddScoped<ISkillCongViec, KyNangCongViecSerVice>();
IServiceCollection serviceCollection11 = builder.Services.AddScoped<ICongViec, CongViecService>();
IServiceCollection serviceCollection12 = builder.Services.AddScoped<IKinhNghiemNguoiXinViec,KinhNghiemNguoiXinViecService>();
IServiceCollection serviceCollection13 = builder.Services.AddScoped<Ikinhnghiemcongviec, KinhNghiemCongViecService>();
IServiceCollection serviceCollection14 = builder.Services.AddScoped<ILoginService, LoginService>();
IServiceCollection serviceCollection15 = builder.Services.AddScoped<ILoginRepository, LoginRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

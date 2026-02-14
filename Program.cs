using Microsoft.EntityFrameworkCore; 
using KutuphaneApi.Data; 
var builder = WebApplication.CreateBuilder(args); 
// Controllers (API endpoint'leri) 
builder.Services.AddControllers(); 
// Swagger 
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); 
// DbContext 
builder.Services.AddDbContext<AppDbContext>(options => 
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")) 
); 
var app = builder.Build(); 
// Swagger UI 
if (app.Environment.IsDevelopment()) 
{ 
app.UseSwagger(); 
app.UseSwaggerUI(); 
} 
app.UseHttpsRedirection(); 
// (Auth kullanmıyorsan bile dursun, sorun çıkarmaz) 
app.UseAuthorization(); 
// Controller route'larını devreye alır (EN KRİTİK SATIR) 
app.MapControllers(); 
app.Run();




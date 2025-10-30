using DashBourd.Models;
using DashBourd.Repositories;
using Ecommerce1.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DashBourd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1️⃣ تسجيل الـ DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            // 2️⃣ إضافة الـ Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // الإيميل لازم يكون فريد
                options.User.RequireUniqueEmail = true;

                // ممكن كمان تضيف إعدادات إضافية (اختياري)
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedEmail = true ;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // 3️⃣ تسجيل الريبوزاتري العام
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // 4️⃣ تفعيل MVC
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // 5️⃣ التعامل مع الأخطاء
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // 6️⃣ إعدادات البايبلاين
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // ✅ ضروري قبل Authorization
            app.UseAuthorization();

            // 7️⃣ مسارات الـ Areas
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            // 8️⃣ المسار الافتراضي (تقدر تحدد منطقة افتراضية لو عايز)
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}",
                defaults: new { area = "Identity" });

            app.Run();
        }
    }
}

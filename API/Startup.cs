using API.Extensions;
using API.Middleware;
using API.SignalR;

// File chứa cấu hình ứng dụng và DI container

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // Cấu hình ứng dụng
        // Tại đây phương thức ConfigureServices được gọi trong quá trình khởi tạo ứng dụng
        // đăng ký các dịch vụ và dependencies mà ứng dụng của bạn cần sử dụng
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationService(_config);
            services.AddControllers();
            services.AddCors();
            services.AddIdentityService(_config);
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Cấu hình Middleware ở phương thức Configure
        // Middleware là các thành phần trung gian được sử dụng 
            // để xử lý yêu cầu HTTP trước khi đến controller
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            // app.UseHttpsRedirection();

            app.UseRouting();

            // sử dụng điều này để cho phêp câc tên miền cheo khi 
                // client truy cập váo 1 địa chỉ nào đó
            app.UseCors(x => x.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("https://localhost:4200"));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<PresenceHub>("hubs/presence");
            });
        }
    }
}
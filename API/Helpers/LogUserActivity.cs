using API.Extensions;
using API.interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            // nếu không được xác thực thì quay lại và không làm gì khác với bộ lọc này
            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;
            
            // nếu được xác thực thì cập nhật thuộc tính hoạt động cuối cùng đó
            var username = resultContext.HttpContext.User.GetUsername();
            // truy cập vào kho lưu trữ
            var repo = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
            // Lưu lại người dùng
            var user = await repo.GetUserByUsernameAsync(username);
            user.LastActive = DateTime.Now;
            await repo.SaveAllAsync();
        }
    }
}
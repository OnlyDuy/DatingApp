// Tại đây sẽ đặt khích thước trang tối đa
// Là số lượng lớn nhất mà tôi sẽ trả lại 
namespace API.Helpers
{
    public class UserParams : PaginationParams
    {
        public string CurrentUsername { get; set; } = "";
        public string Gender { get; set; } = "";
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 150;
        public string OrderBy { get; set; } = "lastActive";
    }
}
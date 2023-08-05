namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        // Phương thức tính tuổi dựa trên ngày sinh (bod) được cung cấp
        public static int CalculateAge(this DateTime bod)
        {   
            // Lấy ra Ngày Tháng Năm của hôm nay
            var today = DateTime.Today;
            // Tính tuổi: Năm hiện tại - Năm nay
            var age = today.Year - bod.Year;
            // Tuy nhiên nếu ngày sinh chưa đến ngày hôm nay trong năm nay 
            // Thì chưa đến sinh nhật vào năm nay => Tuổi giảm đi 1
            if (bod.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
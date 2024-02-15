namespace BlazorApp2.DTOs
{
    public class RequestModel
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
        public int? ClientId { get; set; }
        public int? RoleCategoryId { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? OrgId { get; set; }
    }
}

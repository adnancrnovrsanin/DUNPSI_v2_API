namespace API.DTOs
{
    public class CompanyRegisterResponse
    {
        public UserDto User { get; set; }
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Web { get; set; }
    }
}
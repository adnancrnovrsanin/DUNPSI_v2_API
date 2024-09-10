namespace Domain
{
    public class ProductManager
    {
        public Guid Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
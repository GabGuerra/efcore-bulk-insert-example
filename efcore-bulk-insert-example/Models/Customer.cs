namespace efcore_bulk_insert_example.Models
{
    public class Customer : BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string SSN { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}

namespace Api.DTOs
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressDto[] Addresses { get; set; }
    }
}

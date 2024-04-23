namespace Api.DTOs
{
    public class EditPersonalInfoRequest
    {
        public string Name { get; set; }
        public AddressDto[] Addresses { get; set; }
    }
}
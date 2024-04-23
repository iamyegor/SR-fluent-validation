namespace Api.DTOs
{
    public class GetResonse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public AddressDto[] Addresses { get; set; }
        public CourseEnrollmentDto[] Enrollments { get; set; }
    }
}
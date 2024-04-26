namespace DomainModel.DomainErrors;

public static partial class Errors
{
    public static class Phone
    {
        public static Error IsRequired()
        {
            return new Error("phone.required", "Phone is required");
        }
    }
}

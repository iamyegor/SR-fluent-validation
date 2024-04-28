namespace DomainModel.DomainErrors;

public static partial class Errors
{
    public static class RequestBody
    {
        public static Error IsRequired()
        {
            return new Error("request.body.is.required", "Non-empty request body is required");
        }
    }
}

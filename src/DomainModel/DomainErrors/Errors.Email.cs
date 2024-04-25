using System.Collections.Generic;

namespace DomainModel.DomainErrors;

public static partial class Errors
{
    public static class Email
    {
        public static Error IsRequired()
        {
            return new Error("email.required", "Email is required");
        }

        public static Error IsTooLong(string email)
        {
            var details = new Dictionary<string, object?>() { ["email"] = email };
            return new Error("email.too.long", "Email is too long", details);
        }

        public static Error HasInvalidSignature(string email)
        {
            var details = new Dictionary<string, object?>() { ["email"] = email };
            return new Error("email.invalid.signature", "Email has invalid signature", details);
        }
    }
}

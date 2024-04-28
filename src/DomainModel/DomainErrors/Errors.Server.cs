using System.Collections.Generic;

namespace DomainModel.DomainErrors;

public static partial class Errors
{
    public static class Server
    {
        public static Error InternalServerError(string errorMessage)
        {
            return new Error("internal.server.error", errorMessage);
        }
    }
}

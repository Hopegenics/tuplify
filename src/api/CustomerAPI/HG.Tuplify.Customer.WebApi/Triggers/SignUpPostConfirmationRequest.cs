using System.Collections.Generic;

namespace HG.Tuplify.Customer.WebApi.Triggers
{
    public record SignUpPostConfirmationRequest
    {
        public IDictionary<string, string> UserAttributes { get; set; }
        
        public IDictionary<string, string> ClientMetadata { get; set; }
    }
}
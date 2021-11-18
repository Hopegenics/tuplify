using System.Collections.Generic;

namespace HG.Tuplify.CognitoTrigger.Service.Models
{
    public record SignUpPostConfirmationRequest(
        IDictionary<string, object> UserAttributes,
        IDictionary<string, object> ClientMetadata);
}
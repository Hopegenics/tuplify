using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;

namespace HG.Tuplify.Customer.WebApi.Triggers
{
    public class SignUpPostConfirmationTrigger
    {
        public async Task<SignUpPostConfirmationItem> SignUpPostConfirmationTriggerHandler(SignUpPostConfirmationItem input, ILambdaContext context)
        {

            context.Logger.LogLine($"input is null?: {input == null}");
            context.Logger.LogLine($"input request is null?: {input.Request == null}");

            context.Logger.LogLine($"User Attribute Count?: {input.Request?.UserAttributes?.Count ?? 0}");

            foreach (var (key, value) in input.Request.UserAttributes)
            {
                context.Logger.LogLine($"User attribute Key: {key}, Value: {value}");
            }
            
            context.Logger.LogLine($"Client Metadata Count?: {input.Request?.ClientMetadata?.Count ?? 0}");

            return input;
        }
    }
}

using System.Threading.Tasks;
using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace HG.Tuplify.Customer.WebApi.Triggers
{
    public class SignUpPostConfirmationTrigger
    {
        public async Task<SignUpPostConfirmationItem> SignUpPostConfirmationTriggerHandler(SignUpPostConfirmationItem input, ILambdaContext context)
        {
            if(input == null || 
                input.Request == null || 
                input.Request.UserAttributes == null || 
                !input.Request.UserAttributes.TryGetValue("cognito:user_status", out var userStatus))
            {
                context.Logger.Log($"ERR: input is not correct!");
                return null;
            }

            if(userStatus != "CONFIRMED")
            {
                context.Logger.Log($"WARN: user sign up is not confirmed!");
                return null;
            }

            if(!input.Request.UserAttributes.TryGetValue("user_email", out var userEmail) ||
                !input.Request.UserAttributes.TryGetValue("custom:name", out var name) ||
                !input.Request.UserAttributes.TryGetValue("custom:surname", out var surname))
            {
                context.Logger.Log($"ERR: some of user information is missing!");
                return null;
            }

            context.Logger.Log($"Email: {userEmail}, Name: {name}, Surname: {surname}");

            // dynamodb implementation
            // call repositories

            return input;
        }
    }
}


//context.Logger.LogLine($"input is null?: {input == null}");
//context.Logger.LogLine($"input request is null?: {input.Request == null}");

//context.Logger.LogLine($"User Attribute Count?: {input.Request?.UserAttributes?.Count ?? 0}");

//foreach (var (key, value) in input.Request?.UserAttributes)
//{
//    context.Logger.LogLine($"User attribute Key: {key}, Value: {value}");
//}

//context.Logger.LogLine($"Client Metadata Count?: {input.Request?.ClientMetadata?.Count ?? 0}");


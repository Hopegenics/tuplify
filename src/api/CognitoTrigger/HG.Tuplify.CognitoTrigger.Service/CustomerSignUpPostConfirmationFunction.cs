using Amazon.Lambda.Core;
using HG.Tuplify.CognitoTrigger.Service.DTO;
using HG.Tuplify.CognitoTrigger.Service.Models;
using HG.Tuplify.CognitoTrigger.Persistence.Config;
using HG.Tuplify.CognitoTrigger.Persistence.Models;
using HG.Tuplify.CognitoTrigger.Persistence;
using System.Linq;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace HG.Tuplify.CognitoTrigger.Service
{
    public class CustomerSignUpPostConfirmationFunction
    {        
        /// <summary>
        /// A simple function that takes a string and returns both the upper and lower case version of the string.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public SignUpPostConfirmationItem FunctionHandler(SignUpPostConfirmationItem input, ILambdaContext context)
        {
            //TODO Fuction donus degeri confirmation'i patlatiyor
            
            context.Logger.Log($"{nameof(FunctionHandler)} executing.");

            TuplifyConfiguration.ConfigureSettings();

            if (input?.Request?.UserAttributes?.TryGetValue("cognito:user_status", out var userStatus) != true)
            {
                context.Logger.Log($"ERR: input is not correct!");
                return null;
            }

            if (!userStatus.ToString().Equals("CONFIRMED"))
            {
                context.Logger.Log($"WARN: user sign up is not confirmed!");
                return null;
            }

            var isEmailExists = input.Request.UserAttributes.TryGetValue("email", out object userEmail);
            var isNameExists = input.Request.UserAttributes.TryGetValue("name", out object name);
            var isSurnameExists = input.Request.UserAttributes.TryGetValue("family_name", out object surname);

            if (!isEmailExists || !isNameExists || !isSurnameExists)
            {
                var missingInfo = $"{(!isEmailExists ? "Email " : null)}" +
                    $"{(!isNameExists ? "Name " : null)}" +
                    $"{(!isSurnameExists ? "Surname " : null)}";

                context.Logger.Log($"ERR: {missingInfo} is/are missing!");

                return null;
            }

            context.Logger.Log($"Email: {userEmail}, Name: {name}, Surname: {surname} received.");

            var customer = new CustomerDto(userEmail.ToString(), name.ToString(), surname.ToString());

            SaveCustomerInfo(customer, context);

            context.Logger.Log($"{nameof(FunctionHandler)} executed.");

            return input;
        }

        private void SaveCustomerInfo(CustomerDto customer, ILambdaContext context)
        {
            context.Logger.Log($"{nameof(SaveCustomerInfo)} executing");

            var customerInfo = new CustomerInfo
            {
                CustomerEmail = customer.Email,
                CustomerName = customer.Name,
                CustomerSurname = customer.Surname
            };

            using var dbContext = new TuplifyDbContext();

            dbContext.Database.EnsureCreated();

            var isCustomerExist = dbContext.CustomerInfos.Any(ci => ci.CustomerEmail.Equals(customer.Email));

            if(!isCustomerExist)
            {
                dbContext.CustomerInfos.Add(customerInfo);

                dbContext.SaveChanges();

                context.Logger.Log($"Customer Saved with: {customerInfo}");
            }
            else
            {
                context.Logger.Log($"Customer already exists, Save is not done. Existing record :{customerInfo}");
            }

            context.Logger.Log($"{nameof(SaveCustomerInfo)} executed");
        }
    }
}

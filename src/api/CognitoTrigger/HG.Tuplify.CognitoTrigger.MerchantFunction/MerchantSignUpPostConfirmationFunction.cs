using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace HG.Tuplify.CognitoTrigger.MerchantFunction
{
    public class MerchantSignUpPostConfirmationFunction
    {
        
        /// <summary>
        /// A simple function that takes a string and returns both the upper and lower case version of the string.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Casing MerchantSignUpPostConfirmationFunctionHandler(string input, ILambdaContext context)
        {
            return new Casing(input?.ToLower(), input?.ToUpper());
        }
    }

    public record Casing(string Lower, string Upper);
}

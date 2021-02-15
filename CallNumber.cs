using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.TwiML;
using System.Net.Http;
using System.Net;
using System.Text;

namespace Company.Function
{
    public static class CallNumber
    {
        [FunctionName("CallNumber")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNTSID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTHTOKEN");
            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber(Environment.GetEnvironmentVariable("NUMBERTOCALL"));
            var from = new PhoneNumber(Environment.GetEnvironmentVariable("TWILIO_PHONENO"));
            var call = CallResource.Create(to, from,
            twiml: new Twiml("<Response><Say>Hey there. Hope you're doing well. Well done getting this call running. Use this opportunity to get away from here.</Say></Response>"));
            Console.WriteLine(call.Sid);

            var res = JsonConvert.SerializeObject(call);

            return new HttpResponseMessage(HttpStatusCode.OK) {
                Content = new StringContent(res, Encoding.UTF8, "application/json")
            };
        }
    }
}

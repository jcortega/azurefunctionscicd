using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http.Internal;
using System.IO;
using azurefunctionscicd;
using Microsoft.AspNetCore.Mvc;

namespace azurefunctionscicd.test
{
    [TestClass]
    public class HttpTriggerTest : FunctionTestHelper.FunctionTest
    {
        [TestMethod]
        public async Task Request_With_Query()
        {
            var query = new Dictionary<String, StringValues>();
            query.TryAdd("name", "jerome");
            var body = "";

            var result = HttpTrigger.Run(req: HttpRequestSetup(query, body), log: log);
            var resultObject = (OkObjectResult)result;
            Assert.AreEqual("Hi jerome.", resultObject.Value);
            
        }

        [TestMethod]
        public async Task Request_Without_Query()
        {
            var query = new Dictionary<String, StringValues>();
            var body = "{\"name\":\"ortega\"}";

            var result = HttpTrigger.Run(HttpRequestSetup(query, body), log);
            var resultObject = (OkObjectResult)result;
            Assert.AreEqual("Hi ortega.", resultObject.Value);

        }

        [TestMethod]
        public async Task Request_Without_Query_And_Body()
        {
            var query = new Dictionary<String, StringValues>();
            var body = "";
            var result = HttpTrigger.Run(HttpRequestSetup(query, body), log);
            var resultObject = (BadRequestObjectResult)result;
            Assert.AreEqual("Please pass a name on the query string or in the request body", resultObject.Value);
        }
    }
}

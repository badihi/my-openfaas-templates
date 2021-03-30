using System;
using Xunit;
using Function;
using Moq;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http.Internal;

namespace FunctionTest
{
    public class FunctionHandlerTest
    {
        private MemoryStream _memoryStream;
        private Mock<HttpRequest> _mockRequest;
        private FunctionHandler _handler;
        public FunctionHandlerTest()
        {
            _handler = new FunctionHandler();
            _mockRequest = CreateMockRequest(new TestBodyData
            {

            }, new Dictionary<string, StringValues>
            {
                { "param1", "" }
            });
        }

        [Fact]
        public async Task HandleTest()
        {
            var result = await _handler.Handle(_mockRequest.Object);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.Status);
        }

        private Mock<HttpRequest> CreateMockRequest(object body, Dictionary<string, StringValues> query = null)
        {
            var json = JsonConvert.SerializeObject(body);
            var byteArray = Encoding.ASCII.GetBytes(json);

            _memoryStream = new MemoryStream(byteArray);
            _memoryStream.Flush();
            _memoryStream.Position = 0;

            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Body).Returns(_memoryStream);

            if (query != null)
            {
                mockRequest.Setup(i => i.Query).Returns(new QueryCollection(query));
            }

            return mockRequest;
        }
    }

    public class TestBodyData
    {

    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Function
{
    public abstract class Response
    {
        public HttpStatusCode Status { get; private set; }
        public Response(HttpStatusCode status)
        {
            Status = status;
        }

        public async Task ApplyToHttpResponse(HttpResponse response)
        {
            response.StatusCode = (int)Status;
            await _SetBody(response);
        }

        protected abstract Task _SetBody(HttpResponse response);
    }

    public class Text : Response
    {
        public string Body { get; private set; }
        public string ContentType { get; private set; }
        public Text(HttpStatusCode statusCode, string text, string contentType)
            : base (statusCode)
        {
            Body = text;
            ContentType = contentType;
        }

        protected override async Task _SetBody(HttpResponse response)
        {
            response.ContentType = ContentType;
            await response.WriteAsync(Body, Encoding.UTF8);
        }
    }

    public class File : Response
    {
        public byte[] Contents { get; private set; }
        public string ContentType { get; private set; }
        public File(HttpStatusCode statusCode, byte[] contents, string contentType)
            : base(statusCode)
        {
            Contents = contents;
            ContentType = contentType;
        }

        protected override async Task _SetBody(HttpResponse response)
        {
            response.ContentType = ContentType;
            await response.Body.WriteAsync(Contents, 0, Contents.Length);
        }
    }

    public class Redirect : Response
    {
        public string Url { get; private set; }
        public Redirect(string url)
            : base(HttpStatusCode.MovedPermanently)
        {
            Url = url;
        }

        protected override Task _SetBody(HttpResponse response)
        {
            response.Redirect(Url);
            return Task.CompletedTask;
        }
    }
}

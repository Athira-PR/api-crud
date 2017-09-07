using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API_CRUD_EXAMPLE
{
    public class customControl : IHttpActionResult

    {
        string _value;
        HttpStatusCode _code;
        HttpRequestMessage _message;
        public customControl(string value,HttpStatusCode code,HttpRequestMessage message)
        {
            _value = value;
            _code = code;
            _message = message;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(_value),
                RequestMessage = _message,
                StatusCode= _code

            };
            return Task.FromResult(response);

        }
    }
}
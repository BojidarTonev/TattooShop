using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TattooShop.Web.Tests
{
    public class Class1 : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _server;

        public Class1(WebApplicationFactory<Startup> server)
        {
            this._server = server;
        }

        [Fact]
        public void ProductOrderRequireAuthorization()
        {
            var client = this._server.CreateClient();
            var response = client.GetAsync("/Products/products/Order")
        }
    }
}

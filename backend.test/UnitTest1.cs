using backend.Controllers;
using backend.Model;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using FluentAssertions;

namespace backend.tests
{
    public class UnitTest1
    {
        [Fact]
        public void RetornarPessoa_DeveRetornarTodasPessoas()
        {
            var controller = new HomeController();

            var result = controller.RetornarPessoa() as OkObjectResult;

            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
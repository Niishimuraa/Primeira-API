using backend.Controllers;
using backend.Model;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using FluentAssertions;
using System.Net.WebSockets;
using backend.DTO;

namespace backend.tests
{
    public class UnitTest1
    {
        private HomeController _controller;
        public UnitTest1()
        {
            _controller = new HomeController();
        }

        [Fact]
        public void RetornarPessoa_DeveRetornarTodasPessoas()
        {
            //act
            var result = _controller.RetornarPessoa() as OkObjectResult;
            var pessoa = result?.Value as List<Pessoa>;

            //assert
            result.Should().BeOfType<OkObjectResult>();
            pessoa.Should().HaveCount(HomeController.Pessoas.Count);
        }

        [Theory]
        [InlineData("Henri")]
        [InlineData("Ju")]
        [InlineData("John")]
        public void RetornarNome_DeveRetornarUmaPessoa(string nome)
        {
            //act
            var result = _controller.RetornarNome(nome) as OkObjectResult;
            var pessoa = result?.Value as Pessoa;

            //assert
            result.Should().BeOfType<OkObjectResult>();
            pessoa.Should().NotBeNull();
            pessoa?.Nome.Should().StartWith(nome);
        }

        [Theory]
        [InlineData("Joj")]
        [InlineData("Jonh")]
        [InlineData("123")]
        public void RetornarNome_NaoDeveRetornarUmaPessoa(string nome)
        {
            //act
            var result = _controller.RetornarNome(nome) as OkObjectResult;
            var pessoa = result?.Value as Pessoa;

            //assert
            pessoa.Should().BeNull();
            pessoa?.Nome.Should().NotStartWith(nome);
        }

        [Theory]
        [InlineData("Daniel")]
        [InlineData("Rodrigo")]
        [InlineData("Ana")]
        public void AdicionarPessoa_DeveAdicionarUmaPessoaNaLista(string nome)
        {
            //arrange
            var pessoaDTO = new PessoaDTO { Nome = nome };

            //act
            _controller.AdicionarPessoa(pessoaDTO);
            var pessoaAdicionada = HomeController.Pessoas.Find(x =>  x.Nome == nome);

            //assert
            pessoaAdicionada.Should().NotBeNull();
            pessoaAdicionada.Nome.Should().Be(nome);
        }
    }
}